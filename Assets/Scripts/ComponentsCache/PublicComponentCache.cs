using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class PublicComponentCache : PublicComponentsCacheBase, IBuiltGlobally
{
    [AutomaticSet] [SerializeField] private BuffHandler _buffHandler;
    [AutomaticSet] [SerializeField] private Mover _mover;
    [AutomaticSet] [SerializeField] private HealthChanger _healthChanger;
    [AutomaticSet] [SerializeField] private ClassInformation _classInformation;

    [HideInInspector]
    [SerializeField] private ComponentsCache _roomEventListeners = new ComponentsCache(typeof(IRoomEventListener), true);
    private IPublicRoomEventListener _roomEventListener;

    [HideInInspector]
    [SerializeField]
    private ComponentsCache _appearenceListeners = new ComponentsCache(typeof(IObjectAppearanceListener), true);
    private IObjectAppearanceListener _appearenceListener;

    [HideInInspector]
    [SerializeField]
    private ComponentsCache _levelEventsListeners = new ComponentsCache(typeof(ILevelEventListener), true);
    private ILevelEventListener _levelEventsListener;

    [SerializeField] private bool _shouldListenToAllRooms;

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
        if (typeof(T).Equals(typeof(IPublicRoomEventListener)))
        {
            if (_roomEventListener == null && _roomEventListeners.Count > 0)
            {
                _roomEventListener =
                    new RoomEventListenerCollector(_shouldListenToAllRooms,
                    _roomEventListeners.GetCachedComponets<IRoomEventListener>());
            }
            return _roomEventListener as T;
        }
        if (typeof(T).Equals(typeof(IRoomEventListener)))
        {
            if (_roomEventListener == null && _roomEventListeners.Count > 0)
            {
                _roomEventListener =
                    new RoomEventListenerCollector(_shouldListenToAllRooms, 
                    _roomEventListeners.GetCachedComponets<IRoomEventListener>());
            }
            else
            {
                return null;
            }
            return _roomEventListener.Listener as T;
        }
        if (typeof(T).Equals(typeof(ILevelEventListener)))
        {
            if (_levelEventsListener == null && _levelEventsListeners.Count > 0)
            {
                _levelEventsListener =
                    new LevelEventListenerCollection(_levelEventsListeners.GetCachedComponets<ILevelEventListener>());
            }
            return _levelEventsListener as T;
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
            var listenersList = new List<IObjectAppearanceListener>();
            //Adding a class that will invoke public Appearance actions in ObjectAPI
            listenersList.Add(new ObjectAppearanceListenerInvoker(this));
            listenersList.AddRange(_appearenceListeners.GetCachedComponets<IObjectAppearanceListener>());
            _appearenceListener = 
                new ObjectAppearanceListenerCollection(listenersList);
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

        _levelEventsListeners.RecalculateComponents(gameObject, this);
        EditorUtility.SetDirty(this);
        if (_levelEventsListeners.Count > 0)
        {
            Debug.Log(_levelEventsListeners.Count + " level events listeners were succesfully subscribed");
        }

        foreach (var built in GetComponentsInChildren<MonoBehaviour>())
        {
            BuildType(built.GetType(), built);
            BuildType(built.GetType().BaseType, built);
        }
    }

    private void BuildType(Type type, MonoBehaviour component)
    {
        foreach (var field in type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(field => field.IsDefined(typeof(AutomaticSetAttribute), false)))
        {
            gameObject.ChooseComponentWithWindow(field.FieldType, comp =>
                { field.SetValue(component, comp); EditorUtility.SetDirty(component); }, null);
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

