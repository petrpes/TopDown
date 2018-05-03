using System;

namespace Components.RoomsManager
{
    public interface IRoomsChanger<R> : IDisposable
    {
        R CurrentRoom { get; }
        bool ChangeRoom(R room);

        event Action<R> OnBeforeRoomClosed;
        event Action<R> OnAfterRoomOpened;
    }
}

