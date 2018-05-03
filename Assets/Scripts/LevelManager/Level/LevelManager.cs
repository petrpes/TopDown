using Components.RoomsManager;
using System;

public class LevelManager
{
    public static LevelManager Instance = new LevelManager();

    public ILevel CurrentLevel { get; private set; }
    public readonly RoomsManager<RoomEventType, IRoom, object> RoomsManager;

    public event Action<ILevel> OnAfterLevelCreated;
    public event Action<ILevel> OnBeforeLevelDestroyed;
    public event Action OnAfterLevelStarted;

    private ILevelCreator _levelCreator;
    private ILevelDestroyer _levelDestroyer;
    private ILevelStarter _levelStarter;

    private LevelManager()
    {
        _levelCreator = new TestLevelCreator();
        _levelDestroyer = new TestLevelDestroyer();
        _levelStarter = new TestLevelStarter();

        var transitionHandler = new RoomTransitionProxy<IRoom>();
        RoomsManager = new RoomsManager<RoomEventType, IRoom, object>(transitionHandler,
            mediators: new IRoomsHooksMediator<RoomEventType, IRoom, object>[] 
            {
                //Invoke closed and opened hooks
                new RoomEventsHooksInvoker()
            });
    }

    public void LoadNextLevel(object levelParams)
    {
        if (CurrentLevel != null)
        {
            OnBeforeLevelDestroyed.SafeInvoke(CurrentLevel);

            _levelDestroyer.DestroyCurrentLevel(CurrentLevel, () =>
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
            CurrentLevel = level;

            OnAfterLevelCreated.SafeInvoke(level);

            _levelStarter.StartLevel(CurrentLevel, () =>
            {
                OnAfterLevelStarted.SafeInvoke();
            });
        });
    }
}

