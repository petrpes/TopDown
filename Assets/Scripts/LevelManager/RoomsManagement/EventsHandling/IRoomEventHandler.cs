using System;

public interface IRoomEventHandler : IDisposable
{
    void InvokeRoomEvent(IRoom room, RoomEventType eventType);

    void SubscribeListener(IRoomEventListener listener, IRoom room);
    void UnsubscribeListener(IRoomEventListener listener, IRoom room);
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
    Action<IRoom> this[RoomEventType eventType] { get; }
}

