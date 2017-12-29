using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class TestRoom : MonoBehaviour, IRoom, ICoroutineCollectionWriter<RoomTransitionArguments>
{
    [SerializeField] private Vector2 _size;
    [SerializeField] private ComponentsCache _roomBasicObjects = new ComponentsCache(typeof(RoomContainedObject).Name, true);

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
    }
}

