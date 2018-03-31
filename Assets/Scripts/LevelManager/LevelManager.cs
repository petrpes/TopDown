using System;

public class LevelManager
{
    public static LevelManager Instance = new LevelManager();

    public ILevel CurrentLevel { get { return _currentLevel; } }
    public IRoomContentMananger RoomContentManager { get; private set; }

    public event Action OnAfterLevelCreated;
    public event Action OnBeforeLevelDestroyed;
    public event Action OnAfterLevelStarted;

    private ILevel _currentLevel;
    private IRoom _currentRoom;
    private IRoom _followingRoom;

    private IRoomEventHandler _roomEventHandler;
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
        RoomContentManager = new RoomContentManangerDictionary();
        _roomEventHandler = RoomEventHandler.Instance;
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

    private void ResetRoomData()
    {
        RoomContentManager.Dispose();
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
            ExecuteRoomAction(CurrentRoom, RoomEventType.OnBeforeClose);
        }

        _followingRoom = room;

        _roomTransition.TransitionToRoom(_currentRoom, room, RoomAfterTransitionAction);
    }

    private void RoomAfterTransitionAction()
    {
        _currentRoom = _followingRoom;

        ExecuteRoomAction(CurrentRoom, RoomEventType.OnAfterOpen);
        ExecuteRoomAction(CurrentRoom, RoomEventType.OnStarted);
    }

    private void ExecuteRoomAction(IRoom room, RoomEventType eventType)
    {
        _roomEventHandler.InvokeRoomEvent(CurrentRoom, eventType);
    }
}

