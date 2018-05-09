public interface IRoomTransition : Components.RoomsManager.IRoomTransition<IRoom>
{
}

public interface IRoomEventListener : Components.RoomsManager.IRoomEventListener<RoomEventType, IRoom>
{
}

public interface IRoomTransitionHandler : Components.RoomsManager.IRoomTransitionHandler<IRoom>
{
}

public enum RoomEventType
{
    OnOpen,
    OnClosed,
    OnCleared
}

