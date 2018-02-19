using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

//[RequireComponent(typeof(DoorsHolder))]
public class TestRoom : MonoBehaviour, IRoom, ICoroutineCollectionWriter<RoomTransitionArguments>
{
    [SerializeField] private Vector2 _size;
    [SerializeField] private DoorsHolder _doorsHolder;
    [SerializeField] private ComponentsCache _roomBasicObjects = new ComponentsCache(typeof(RoomContainedObject).Name, true);
    [HideInInspector]
    [SerializeField] private WallsBase _walls;

    private Transform _transform;
    private Rect _rectangle;

    private Action[] _roomActions;
    public Action this[RoomEventType eventType]
    {
        get
        {
            CreateRoomActionsArrayIfNull();
            return _roomActions[(int)eventType];
        }
        set
        {
            CreateRoomActionsArrayIfNull();
            _roomActions[(int)eventType] = value;
        }
    }

    public int ComponentsCount { get { return _roomBasicObjects.Count; } }

    public void SubscribeAllObjects()
    {
        foreach (var roomObject in _roomBasicObjects.GetCachedComponets<RoomContainedObject>())
        {
            roomObject.SetRoomInInspector(this);
        }
    }

    private void CreateRoomActionsArrayIfNull()
    {
        if (_roomActions == null)
        {
            _roomActions = new Action[Enum.GetValues(typeof(RoomEventType)).Length];
        }
    }

    public Rect Rectangle
    {
        get
        {
            if (_transform == null)
            {
                _transform = transform;
            }
            if (_rectangle == Rect.zero)
            {
                _rectangle = new Rect(_transform.position.x - _size.x / 2,
                                      _transform.position.y - _size.y / 2,
                                      _size.x,
                                      _size.y);
            }

            return _rectangle;
        }
    }

    public IDoorsHolder DoorsHolder
    {
        get
        {
            return _doorsHolder;
        }
    }

    public IEnumerator Coroutine(Action onComplete, RoomTransitionArguments args)
    {
        if (args.OldRoom != null && Equals(args.OldRoom))
        {
            gameObject.SetActive(false);
        }
        if (args.NewRoom != null && Equals(args.NewRoom))
        {
            gameObject.SetActive(true);
        }
        onComplete.Invoke();

        yield return null;
    }

    private void Awake()
    {
        RoomTransitionInvoker.Instance.SubscribeCoroutine(this);
    }
}


[CustomEditor(typeof(TestRoom))]
public class TestRoomEditor : Editor
{
    private int _previousCount = 0;
    private IWallsCreator _wallsCreator = new WallsCreator();

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var basicContent = serializedObject.targetObject as TestRoom;
        int newCount = basicContent.ComponentsCount;
        if (newCount != _previousCount)
        {
            basicContent.SubscribeAllObjects();

            _previousCount = newCount;
        }

        if (GUILayout.Button("Build Walls"))
        {
            BuildWalls();
        }
    }

    private void BuildWalls()
    {
        var walls = this.Property("_walls");
        var room = target as TestRoom;

        if (walls.objectReferenceValue != null)
        {
            DestroyImmediate((walls.objectReferenceValue as MonoBehaviour).gameObject);
        }
        walls.objectReferenceValue = _wallsCreator.CreateWalls(room.Rectangle, RoomConsts.WallsSize, 
            room.gameObject.transform);

        serializedObject.ApplyModifiedProperties();
    }
}

