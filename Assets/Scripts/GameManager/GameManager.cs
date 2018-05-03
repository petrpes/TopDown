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

	void Start ()
    {
        Instance = this;

        ConnectManagers();

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
        if (LevelAPIs.CurrentRoom != null)
        {
            SceneObjectsMananger.Instance.SpawnManager.AfterObjectSpawned += (obj) =>
            {
                LevelAPIs.RoomContent.AddObject(LevelAPIs.CurrentRoom, obj);
            };
            SceneObjectsMananger.Instance.SpawnManager.BeforeObjectDespawned += (obj) =>
            {
                LevelAPIs.RoomContent.RemoveObject(LevelAPIs.CurrentRoom, obj);
            };
        }

        //Set rooms enabled (disabled) on open (close)
        new OnTransitionSetRoomActive().Connect(LevelManager.Instance.RoomsManager);
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

