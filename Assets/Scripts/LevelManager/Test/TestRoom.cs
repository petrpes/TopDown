using System;
using UnityEditor;
using UnityEngine;

public class TestRoom : MonoBehaviour, IRoom, IRoomTransition
{
    [SerializeField] private Vector2 _size;
    [SerializeField] private DoorsHolder _doorsHolder;

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

    private void Awake()
    {
        RoomTransitionProxy.Instance.SubscribeTransitionObject(this);
    }

    private void OnDestroy()
    {
        RoomTransitionProxy.Instance.UnsubscribeTransitionObject(this);
    }

    public void TransitionToRoom(IRoom oldRoom, IRoom newRoom, Action onComplete)
    {
        if (oldRoom != null && Equals(oldRoom))
        {
            gameObject.SetActive(false);
        }
        if (newRoom != null && Equals(newRoom))
        {
            gameObject.SetActive(true);
        }
        onComplete.Invoke();
    }
}


[CustomEditor(typeof(TestRoom))]
public class TestRoomEditor : Editor
{
    private int _previousCount = 0;
    private IWallsColliderCreator _collidersCreator = new WallsColliderCreator();
    private TestRoom BasicObject { get { return this.BasicObject<TestRoom>(); } }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Subscribe objects to room"))
        {
            SubscribeObjects();
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Build Walls"))
        {
            BuildWalls();
        }
    }

    private void SubscribeObjects()
    {
        int objectSubscribed = 0;

        foreach (var component in
            BasicObject.gameObject.GetComponentsExtended<IRoomContainedObject>(true))
        {
            component.DefaultRoom = BasicObject;
            EditorUtility.SetDirty(component as Component);

            objectSubscribed++;
        }

        Debug.Log(objectSubscribed + " objects was successfully subscribed");
    }

    private void BuildWalls()
    {
        var walls = this.Property("_walls");
        var room = target as TestRoom;

        if (room.DoorsHolder == null)
        {
            var doorsHolder = BasicObject.gameObject.GetComponent<DoorsHolder>();

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

