using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DoorsHolder : MonoBehaviour, IDoorsHolder
{
    [SerializeField] private Door _doorPrefab;
    [HideInInspector]
    [SerializeField] private IDoor[] _doors;

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

    public IEnumerable<IDoor> GetDoors(Func<IDoor, bool> predicate = null)
    {
        foreach (var door in _doors)
        {
            if (predicate == null || predicate(door))
            {
                yield return door;
            }
        }
    }
}

[CustomEditor(typeof(DoorsHolder))]
public class DoorsHolderEditor : Editor
{
    private Door _defaultDoor;
    private Orientation _defaultOrientation;
    private Vector3 _holderPosition;
    private Vector3 _editorPosition;

    private Door[] Doors
    {
#region doors
        get
        {
            var arrayProperty = serializedObject.FindProperty("_doors");
            var length = arrayProperty.arraySize;
            var doors = new Door[length];
            for (int i = 0; i < length; i++)
            {
                doors[i] = arrayProperty.GetArrayElementAtIndex(i).
                    objectReferenceValue as Door;
            }
            return doors;
        }
        set
        {
            var arrayProperty = serializedObject.FindProperty("_doors");
            var length = arrayProperty.arraySize = value.Length;

            for (int i = 0; i < length; i++)
            {
                arrayProperty.GetArrayElementAtIndex(i).
                    objectReferenceValue = value[i];
            }
            serializedObject.ApplyModifiedProperties();
        }
#endregion
    }

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

        _defaultDoor = EditorGUILayout.ObjectField(_defaultDoor, typeof(Door), true) as Door;
        _defaultOrientation = (Orientation)EditorGUILayout.EnumPopup(_defaultOrientation);
        var holderPosition = EditorGUILayout.Vector3Field("", _holderPosition);
        ChangeHolderPosition(holderPosition);

        if (GUILayout.Button("Add door"))
        {
            AddDoor(_defaultDoor, _defaultOrientation, _holderPosition);
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void OnSceneGUI()
    {
        EditorGUI.BeginChangeCheck();

        var holderPosition = Handles.PositionHandle(_holderPosition, Quaternion.identity);
        ChangeHolderPosition(holderPosition);
    }

    private Orientation _previousOrientation;
    private bool _isStarted = false;

    private void ChangeHolderPosition(Vector3 position)
    {
        if (_holderPosition.Equals(position) &&
            _previousOrientation.Equals(_defaultOrientation) &&
            _isStarted)
        {
            return;
        }

        _isStarted = true;
        _previousOrientation = _defaultOrientation;
        float holderOffset;
        var rect = Room.Shape.Rectangle;

        switch (_defaultOrientation)
        {
            case Orientation.Top:
                holderOffset = rect.yMax;
                position.y = holderOffset;
                break;
            case Orientation.Right:
                holderOffset = rect.xMax;
                position.x = holderOffset;
                break;
            case Orientation.Bottom:
                holderOffset = rect.yMin;
                position.y = holderOffset;
                break;
            case Orientation.Left:
                holderOffset = rect.xMin;
                position.x = holderOffset;
                break;
        }
        _holderPosition = position;
        Repaint();
    }

    private void AddDoor(Door prefab, Orientation orient, Vector3 position)
    {
        var door = Instantiate(prefab.gameObject, position, Quaternion.identity, 
            Holder.gameObject.transform).GetComponent<Door>();
        var doorsList = new List<Door>(Doors);
        doorsList.Add(door);
        Doors = doorsList.ToArray();
    }

    private void DrawDoors()
    {
        var doors = Doors;
        for (int i = 0; i < doors.Length; i++)
        {
            DrawDoor(doors[i], i);
        }
    }

    private void DrawDoor(Door door, int doorId)
    {
        EditorGUILayout.LabelField("1");
    }
}

