using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private PublicComponentCache _player;

    public PublicComponentCache Player
    {
        get
        {
            return _player;
        }
    }

    IRoom[] _rooms;

    private void Awake()
    {
        Instance = this;
        ConnectManagers();
    }

    void Start ()
    {
        LevelManager.Instance.LoadNextLevel(null);

        _rooms = new IRoom[TestLevelMap.Instance.RoomsCount];
        int i = 0;
        foreach (IRoom room in LevelManager.Instance.CurrentLevel.LevelMap.GetRooms())
        {
            _rooms[i] = room;
            i++;
        }
    }

    private void ConnectManagers()
    {
        //when object added to the room - subscribe it to room's events if it listens only for one room's events
        var roomMananager = LevelManager.Instance.RoomsManager;
        new RoomListenersMediator().Connect(roomMananager);

        //On level created - add default rooms' objects to content
        LevelManager.Instance.OnAfterLevelCreated += (level) =>
        {
            foreach (IRoom room in level.LevelMap.GetRooms())
            {
                foreach (var component in (room as TestRoom).DefaultObjects)
                {
                    LevelAPIs.RoomContent.AddObject(room, component.gameObject);
                }
            }
        };

        //On object spawned - add to room's content
        SceneObjectsMananger.Instance.SpawnManager.AfterObjectSpawned += (obj) =>
        {
            if (LevelAPIs.CurrentRoom == null)
            {
                return;
            }
            var cache = obj.GetLevelObjectComponent<PublicComponentsCacheBase>();
            if (cache != null)
            {
                LevelAPIs.RoomContent.AddObject(LevelAPIs.CurrentRoom, cache);
            }
        };
        SceneObjectsMananger.Instance.SpawnManager.BeforeObjectDespawned += (obj) =>
        {
            if (LevelAPIs.CurrentRoom == null)
            {
                return;
            }
            var cache = obj.GetLevelObjectComponent<PublicComponentsCacheBase>();
            if (cache != null)
            {
                LevelAPIs.RoomContent.RemoveObject(LevelAPIs.CurrentRoom, cache);
            }
        };

        //On object created - subscribe to all rooms events if it listens all rooms
        SceneObjectsMananger.Instance.AppearanceHooks.OnAppearanceAction += (type, obj) =>
        {
            var cache = obj.GetLevelObjectComponent<PublicComponentsCacheBase>();
            var listener = cache == null ? null : cache.GetCachedComponent<IRoomEventListener>();
            if (cache != null && cache.ShouldListenAllRoomsEvents() && listener != null)
            {
                if (type.Equals(ObjectAppearanceType.Created))
                {
                    LevelAPIs.RoomSubscriber.SubscribeListener(listener, null);
                }
                else if (type.Equals(ObjectAppearanceType.Destroyed))
                {
                    LevelAPIs.RoomSubscriber.UnsubscribeListener(listener, null);
                }
            }
        };

        //TODO on create
        //On object appeared - subscribe to level events
        SceneObjectsMananger.Instance.AppearanceHooks.OnAppearanceAction += (type, obj) =>
        {
            var cache = obj.GetLevelObjectComponent<PublicComponentsCacheBase>();
            var listener = cache == null ? null : cache.GetCachedComponent<ILevelEventListener>();
            if (cache != null && listener != null)
            {
                if (type.Equals(ObjectAppearanceType.Appeared))
                {
                    LevelManager.Instance.OnAfterLevelStarted += listener.OnLevelStarted;
                    LevelManager.Instance.OnAfterLevelCreated += listener.OnLevelCreated;
                    LevelManager.Instance.OnAfterLevelStarted += listener.OnLevelStarted;
                }
                else if (type.Equals(ObjectAppearanceType.Disappeared))
                {
                    LevelManager.Instance.OnAfterLevelStarted -= listener.OnLevelStarted;
                    LevelManager.Instance.OnAfterLevelCreated -= listener.OnLevelCreated;
                    LevelManager.Instance.OnAfterLevelStarted -= listener.OnLevelStarted;
                }
            }
        };

        //Set rooms enabled (disabled) on open (close)
        new OnTransitionSetRoomActive().Connect(roomMananager);
    }

    private void Update()
    {
        for (int i = (int)KeyCode.Alpha0; i <= (int)KeyCode.Alpha9; i++)
        {
            if (Input.GetKeyDown((KeyCode)i))
            {
                int roomId = i - (int)KeyCode.Alpha0;
                LevelAPIs.ChangeRoom(_rooms[roomId]);
                break;
            }
        }
    }
}

