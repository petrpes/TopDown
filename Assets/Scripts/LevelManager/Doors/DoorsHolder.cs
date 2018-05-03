using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DoorsHolder : MonoBehaviour, IDoorsHolder
{
    [SerializeField] private Door _doorPrefab;
    [HideInInspector]
    [SerializeField] private Door[] _doors;
    [HideInInspector]
    [SerializeField] private DoorPosition[] _doorsPositions;

    private IRoom _room;

    public IRoom Room
    {
        get
        {
            if (_room == null)
            {
                _room = GetComponent<IRoom>();
            }
            return _room;
        }
    }

    public IEnumerable<IDoor> GetDoors(Func<IDoor, DoorPosition, bool> predicate = null)
    {
        if (_doors != null)
        {
            for (int i = 0; i < _doors.Length; i++)
            {
                if (predicate == null || predicate(_doors[i], _doorsPositions[i]))
                {
                    yield return _doors[i];
                }
            }
        }
    }

    public DoorPosition GetDoorPosition(IDoor door)
    {
        return _doorsPositions[Array.IndexOf(_doors, door)];
    }

#if UNITY_EDITOR
    public void AddDoor(Door door, DoorPosition position)
    {
        if (_doors == null)
        {
            _doors = new Door[] { door };
        }
        else
        {
            var list = new List<Door>(_doors);
            list.Add(door);
            _doors = list.ToArray();
        }


        if (_doorsPositions == null)
        {
            _doorsPositions = new DoorPosition[] { position };
        }
        else
        {
            var listPosition = new List<DoorPosition>(_doorsPositions);
            listPosition.Add(position);
            _doorsPositions = listPosition.ToArray();
        }
    }

    public void RemoveDoor(Door door)
    {
        if (_doors == null)
        {
            return;
        }

        var list = new List<Door>(_doors);
        var index = Array.IndexOf(_doors, door);
        list.Remove(door);
        _doors = list.ToArray();

        var listPosition = new List<DoorPosition>(_doorsPositions);
        listPosition.RemoveAt(index);
        _doorsPositions = listPosition.ToArray();

        DestroyImmediate(door.gameObject);
    }

    public void RemoveAll()
    {
        for (int i = 0; i < _doors.Length; i++)
        {
            if (_doors[i] != null)
            {
            DestroyImmediate((_doors[i] as Door).gameObject);
            }
        }

        _doors = null;
        _doorsPositions = null;
    }
#endif
}

[CustomEditor(typeof(DoorsHolder))]
public class DoorsHolderEditor : Editor
{
    private Door _defaultDoor;
    private int _lineId;
    private float _positionOnTheLine;
    private TestRoom _targetRoom;

    private DoorsHolder Holder
    {
        get
        {
            return (target as DoorsHolder);
        }
    }

    private IRoom Room
    {
        get
        {
            return Holder.Room;
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DrawButton();
        DrawDoors();
    }

    private void DrawButton()
    {
        if (_defaultDoor == null)
        {
            _defaultDoor = serializedObject.FindProperty("_doorPrefab").
                objectReferenceValue as Door;
        }

        if (_defaultDoor == null)
        {
            return;
        }

        var prevLineId = _lineId;
        _lineId = EditorGUILayout.IntSlider("Wall id", _lineId, 0, Room.Shape.LinesCount - 1);

        var prevPositionOnTheLine = _positionOnTheLine;
        var lowestPosition = _defaultDoor.Width / 2f;
        var longestPosition = Room.Shape[_lineId].Line.Length - _defaultDoor.Width / 2f;
        _positionOnTheLine = EditorGUILayout.Slider("Position on the line", _positionOnTheLine, lowestPosition, longestPosition);
        _targetRoom = EditorGUILayout.ObjectField("Target Room", _targetRoom, typeof(TestRoom), true) as TestRoom;

        if (prevLineId != _lineId || prevPositionOnTheLine != _positionOnTheLine)
        {
            SceneView.RepaintAll();
        }

        if (GUILayout.Button("Add door"))
        {
            AddDoor(_defaultDoor, _lineId, _positionOnTheLine, 0, _targetRoom);
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void OnSceneGUI()
    {
        Vector3 position;
        Quaternion rotation;
        GetPositionRotation(_lineId, _positionOnTheLine, 0, Vector2.right, out position, out rotation);

        var holderPosition = Handles.PositionHandle(position, rotation);
    }

    private void AddDoor(Door prefab, int lineId, float positionOnTheLine, float offset, IRoom room)
    {
        Vector3 position;
        Quaternion rotation;
        GetPositionRotation(lineId, positionOnTheLine, offset, prefab.DefaultOrientation, out position, out rotation);

        var door = Instantiate(prefab.gameObject, position, rotation, 
            Holder.gameObject.transform).GetComponent<Door>();
        var doorPosition = new DoorPosition() { LineId = lineId, PartOfTheLine = positionOnTheLine };
        door.RoomTo = room;

        Holder.AddDoor(door, doorPosition);
    }

    private void GetPositionRotation(int lineId, float positionOnTheLine, float offset, 
        Vector2 defaultOrientation, out Vector3 position, out Quaternion rotation)
    {
        Vector2 normale;
        position = Room.Shape.GetPointOnAPerimeter(lineId, positionOnTheLine, offset, out normale);
        rotation = Quaternion.Euler(0, 0, defaultOrientation.AngleBetween(normale));
    }

    private void DrawDoors()
    {
        int count = 0;

        foreach (var door in Holder.GetDoors())
        {
            count++;
            DrawDoor(door as Door);
        }

        if (count > 0 && GUILayout.Button("Delete all doors"))
        {
            Holder.RemoveAll();
        }
    }

    private void DrawDoor(Door door)
    {
        EditorGUILayout.LabelField(Holder.GetDoorPosition(door).ToString());
        if (GUILayout.Button("Delete"))
        {
            Holder.RemoveDoor(door);
        }
    }
}

