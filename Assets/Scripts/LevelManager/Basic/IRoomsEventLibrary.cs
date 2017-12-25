using System;

public interface IRoomsEventLibrary : IDisposable
{
    void AddRoom(IRoom room);
    InvokeableEvent this[IRoom room, RoomEventType eventType] { get; }
}

public enum RoomEventType
{
    OnAfterOpen,
    OnStarted,
    OnBeforeClose,
    OnClear
}

