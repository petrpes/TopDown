using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class TestRoom : MonoBehaviour, IRoom, ICoroutineCollectionWriter<RoomTransitionArguments>
{
    [SerializeField] private Vector2 _size;
    [SerializeField] private DoorsHolder _doorsHolder;
    [HideInInspector]
    [SerializeField] private ComponentsCache _roomBasicObjects = new ComponentsCache(typeof(RoomContainedObject).Name, true);

    private Transform _transform;
    private Rect _rectangle;
    private Vector2 _perviousSize;
    private Vector2 _perviousPosition;
    private IShape _shape;

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

    public void RecalculateObject()
    {
        _roomBasicObjects.RecalculateComponents(gameObject, this);
    }

    public void SubscribeAllObjects()
    {
        foreach (var roomObject in _roomBasicObjects.GetCachedComponets<RoomContainedObject>())
        {
            roomObject.DefaultRoom = this;
        }
    }

    private void CreateRoomActionsArrayIfNull()
    {
        if (_roomActions == null)
        {
            _roomActions = new Action[Enum.GetValues(typeof(RoomEventType)).Length];
        }
    }

    public IShape Shape
    {
        get
        {
            if (_transform == null)
            {
                _transform = transform;
            }
            if (_shape == null || _rectangle == Rect.zero || 
                !_size.Equals(_perviousSize) || !_perviousPosition.Equals(_transform.position))
            {
                _perviousSize = _size;
                _perviousPosition = _transform.position;

                _rectangle = new Rect(_perviousPosition.x - _size.x / 2,
                                      _perviousPosition.y - _size.y / 2,
                                      _size.x,
                                      _size.y);
                _shape = new ShapeRectangle(_rectangle);
            }

            return _shape;
        }
    }

    public IDoorsHolder DoorsHolder
    {
        get
        {
            return _doorsHolder;
        }
#if UNITY_EDITOR
        set
        {
            _doorsHolder = value as DoorsHolder;
        }
#endif
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
    private IWallsColliderCreator _collidersCreator = new WallsColliderCreator();

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var basicContent = this.BasicObject<TestRoom>();
        if (GUILayout.Button("Subscribe objects to room (" + basicContent.ComponentsCount + ")"))
        {
            basicContent.RecalculateObject();
            basicContent.SubscribeAllObjects();
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Build Walls"))
        {
            BuildWalls();
        }
    }

    private void BuildWalls()
    {
        var walls = this.Property("_walls");
        var room = target as TestRoom;

        if (room.DoorsHolder == null)
        {
            var doorsHolder = this.BasicObject<TestRoom>().gameObject.GetComponent<DoorsHolder>();

            if (doorsHolder != null && 
                EditorUtility.DisplayDialog("DoorsHolder", 
                    "There is seems to be a DoorsHolder component attached to this object. " +
                    "Do you like to set it to this room's DoorsHolder property?",
                    "OK", "Nevermind"))
            {
                room.DoorsHolder = doorsHolder;
            }
        }

        var gameObject = CreateGameObject(room.gameObject);
        _collidersCreator.CreateColliders(room.Shape, room.DoorsHolder, gameObject);
    }

    private GameObject CreateGameObject(GameObject parent)
    {
        var wallGameObject = new GameObject("Walls");

        wallGameObject.transform.parent = parent.transform;
        wallGameObject.tag = "Wall";
        wallGameObject.layer = LayerMask.NameToLayer("Walls");

        var rigidbody = wallGameObject.AddComponent<Rigidbody2D>();
        rigidbody.bodyType = RigidbodyType2D.Static;

        return wallGameObject;
    }
}

