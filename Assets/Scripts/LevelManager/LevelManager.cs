using System;

public class LevelManager
{
    public static LevelManager Instance = new LevelManager();

    public ILevel CurrentLevel { get { return _currentLevel; } }

    public event Action OnAfterLevelCreated;
    public event Action OnBeforeLevelDestroyed;
    public event Action OnAfterLevelStarted;

    private ILevel _currentLevel;
    private IRoom _currentRoom;

    private ILevelCreator _levelCreator;
    private ILevelDestroyer _levelDestroyer;
    private ILevelStarter _levelStarter;
    private IRoomTransition _roomTransition;

    private LevelManager()
    {
        _levelCreator = new TestLevelCreator();
        _levelDestroyer = new TestLevelDestroyer();
        _levelStarter = new TestLevelStarter();
        _roomTransition = RoomTransitionInvoker.Instance;
    }

    public void LoadNextLevel(object levelParams)
    {
        if (_currentLevel != null)
        {
            OnBeforeLevelDestroyed.SafeInvoke();

            _levelDestroyer.DestroyCurrentLevel(_currentLevel, () =>
            {
                CreateLevel(levelParams);
            });
        }
        else
        {
            CreateLevel(levelParams);
        }
    }

    private void CreateLevel(object levelParams)
    {
        _levelCreator.CreateLevel(levelParams, (level) =>
        {
            _currentLevel = level;
            OnAfterLevelCreated.SafeInvoke();

            _levelStarter.StartLevel(_currentLevel, () =>
            {
                OnAfterLevelStarted.SafeInvoke();
            });
        });
    }

    public IRoom CurrentRoom
    {
        get { return _currentRoom; }
        set
        {
            ChangeRoom(value);
        }
    }

    private void ChangeRoom(IRoom room)
    {
        if (_currentRoom != null)
        {
            RoomEventHandler.Instance.InvokeEvent(_currentRoom, RoomEventType.OnBeforeClose);
        }
        _roomTransition.TransitionToRoom(_currentRoom, room, () =>
        {
            _currentRoom = room;
            RoomEventHandler.Instance.InvokeEvent(_currentRoom, RoomEventType.OnAfterOpen);
            RoomEventHandler.Instance.InvokeEvent(_currentRoom, RoomEventType.OnStarted);
        });
    }
}

