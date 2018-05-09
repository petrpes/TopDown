using Components.RoomsManager;

/// <summary>
/// This class will automatically subscribe listeners to rooms' events when it will be 
/// added to any room's content
/// </summary>
public class RoomListenersMediator : RoomsHooksMediator<RoomEventType, IRoom, object>
{
    protected override IRoomEventListener<RoomEventType, IRoom> RetrieveListener(object roomObject)
    {
        return roomObject.GetLevelObjectComponent<IRoomEventListener>();
    }

    protected override bool ShouldListenAllRooms(object roomObject)
    {
        return roomObject.GetLevelObjectComponent<PublicComponentsCacheBase>().ShouldListenAllRoomsEvents();
    }
}

