using System;

public interface IRoomContainedObject
{
    IRoom CurrentRoom { get; }
    event Action<IRoom, IRoom> RoomChanged;
}

