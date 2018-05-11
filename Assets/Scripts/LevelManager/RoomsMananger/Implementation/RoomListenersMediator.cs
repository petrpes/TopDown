using Components.RoomsManager;

/// <summary>
/// This class will automatically subscribe listeners to rooms' events when it will be 
/// added to any room's content
/// </summary>
public class RoomListenersMediator : RoomsHooksMediator<RoomEventType, IRoom, object>
{
    protected override IRoomEventListener<RoomEventType, IRoom> RetrieveListener(object roomObject)
    {
        return roomObject.GetLevelObjectComponent<IPublicRoomEventListener>().Listener;
    }

    protected override bool ShouldListenAllRooms(object roomObject)
    {
        return roomObject.GetLevelObjectComponent<IPublicRoomEventListener>().ShouldListenAllRooms;
    }
}

