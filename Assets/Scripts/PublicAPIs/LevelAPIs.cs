using Components.RoomsManager;

public static class LevelAPIs
{
    public static IRoom CurrentRoom
    {
        get
        {
            return LevelManager.Instance.RoomsManager.RoomsChanger.CurrentRoom;
        }
    }

    public static bool ChangeRoom(IRoom room)
    {
        return LevelManager.Instance.RoomsManager.RoomsChanger.ChangeRoom(room);
    }

    public static IRoomContent<object, IRoom> RoomContent
    {
        get
        {
            return LevelManager.Instance.RoomsManager.Content;
        }
    }

    public static IRoomTransitionHandler<IRoom> TransitionHandler
    {
        get
        {
            return LevelManager.Instance.RoomsManager.TransitionHandler;
        }
    }

    public static IRoomsChanger<IRoom> RoomChanger
    {
        get
        {
            return LevelManager.Instance.RoomsManager.RoomsChanger;
        }
    }

    public static IRoomEventsHooks<RoomEventType, IRoom> RoomEventHooks
    {
        get
        {
            return LevelManager.Instance.RoomsManager.Hooks;
        }
    }
}

