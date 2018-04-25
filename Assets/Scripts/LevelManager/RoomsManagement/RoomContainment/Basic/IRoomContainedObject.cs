using System;

public interface IRoomContainedObject
{
#if UNITY_EDITOR
    IRoom DefaultRoom { set; }
#endif
    IRoom CurrentRoom { get; }
    event Action<IRoom, IRoom> RoomChanged;
}

