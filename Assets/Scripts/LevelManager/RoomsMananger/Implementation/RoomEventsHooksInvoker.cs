using Components.RoomsManager;

/// <summary>
/// This connection will envoke "Open" and "Close" events hooks when rooms are opened and closed
/// </summary>
public class RoomEventsHooksInvoker : IRoomsHooksMediator<RoomEventType, IRoom, object>
{
    public void Connect(RoomsManager<RoomEventType, IRoom, object> manager)
    {
        manager.RoomsChanger.OnAfterRoomOpened += (room) =>
        {
            manager.Hooks.Invoke(room, RoomEventType.OnOpen);
        };
        manager.RoomsChanger.OnBeforeRoomClosed += (room) =>
        {
            manager.Hooks.Invoke(room, RoomEventType.OnClosed);
        };
    }
}

