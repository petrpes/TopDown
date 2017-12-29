using System;

public interface IRoomEventHandler : IDisposable
{
    void InvokeRoomEvent(IRoom room, RoomEventType eventType);

    void SubscribeListener(IRoomEventListener listener, IRoom room);
    void UnsubscribeListener(IRoomEventListener listener, IRoom room);

    void SubscribeListener(IAllRoomsEventListener listener);
    void UnsubscribeListener(IAllRoomsEventListener listener);
}

public enum RoomEventType
{
    OnAfterOpen,
    OnBeforeClose,
    OnClear,
    OnStarted
}

public interface IRoomEventListener
{
    Action this[RoomEventType eventType] { get; }
}

public interface IAllRoomsEventListener
{
    Action<IRoom> this[RoomEventType eventType] { get; }
}

