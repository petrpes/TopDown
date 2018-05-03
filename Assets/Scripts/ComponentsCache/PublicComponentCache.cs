using UnityEditor;
using UnityEngine;

public class PublicComponentCache : PublicComponentsCacheBase
{
    [SerializeField] private BuffHandler _buffHandler;
    [SerializeField] private Mover _mover;
    [SerializeField] private HealthChanger _healthChanger;
    [SerializeField] private ClassInformation _classInformation;

    [SerializeField] private bool _shouldListenAllRooms;
    [HideInInspector]
    [SerializeField] private ComponentsCache _roomEventListeners = new ComponentsCache(typeof(IRoomEventListener), true);
    private IRoomEventListener _roomEventListener;

    [HideInInspector]
    [SerializeField]
    private ComponentsCache _appearenceListeners = new ComponentsCache(typeof(IObjectAppearanceListener), true);
    private IObjectAppearanceListener _appearenceListener;

    public override T GetCachedComponent<T>()
    {
        if (typeof(T).Equals(typeof(BuffHandler)))
        {
            return _buffHandler as T;
        }
        if (typeof(T).Equals(typeof(Mover)))
        {
            return _mover as T;
        }
        if (typeof(T).Equals(typeof(HealthChanger)))
        {
            return _healthChanger as T;
        }
        if (typeof(T).Equals(typeof(ClassInformation)))
        {
            return _classInformation as T;
        }
        if (typeof(T).Equals(typeof(IRoomEventListener)))
        {
            if (_roomEventListener == null && _roomEventListeners.Count > 0)
            {
                _roomEventListener =
                    new RoomEventListenerCollector(_shouldListenAllRooms, _roomEventListeners.GetCachedComponets<IRoomEventListener>());
            }
            return _roomEventListener as T;
        }
        return null;
    }

    [System.NonSerialized] private bool _isStarted = false;
    [System.NonSerialized] private bool _isCreated = false;

    private void Start()
    {
        _isStarted = true;
        InvokeAction(ObjectAppearanceType.Appeared);
    }

    private void Awake()
    {
        if (!_isCreated)
        {
            _isCreated = true;
            InvokeAction(ObjectAppearanceType.Created);
        }
    }

    private void OnDisable()
    {
        if (_isStarted)
        {
            _isStarted = false;
            InvokeAction(ObjectAppearanceType.Disappeared);
        }
    }

    private void OnDestroy()
    {
        if (_isCreated)
        {
            _isCreated = false;
            InvokeAction(ObjectAppearanceType.Destroyed);
        }
    }

    private void InvokeAction(ObjectAppearanceType type)
    {
        if (_appearenceListener == null)
        {
            _appearenceListener = 
                new ObjectAppearanceListenerCollection(_appearenceListeners.GetCachedComponets<IObjectAppearanceListener>());
        }
        if (_appearenceListener != null)
        {
            _appearenceListener.OnAppearanceAction(type);
        }
    }

#if UNITY_EDITOR
    public void Build()
    {
        _roomEventListeners.RecalculateComponents(gameObject, this);
        EditorUtility.SetDirty(this);
        if (_roomEventListeners.Count > 0)
        {
            Debug.Log(_roomEventListeners.Count + " room event listeners were succesfully subscribed");
        }

        _appearenceListeners.RecalculateComponents(gameObject, this);
        EditorUtility.SetDirty(this);
        if (_appearenceListeners.Count > 0)
        {
            Debug.Log(_appearenceListeners.Count + " appearence listeners were succesfully subscribed");
        }
    }
#endif
}

[CustomEditor(typeof(PublicComponentCache))]
public class PublicComponentCacheEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Build object"))
        {
            this.BasicComponent<PublicComponentCache>().Build();
        }
    }
}

