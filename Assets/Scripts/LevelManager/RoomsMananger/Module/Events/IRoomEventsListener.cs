using System;

namespace Components.RoomsManager
{
    public interface IRoomEventListener<E, R> where E : struct, IConvertible
    {
        /// <summary>
        /// If true object will subscribe to events of all rooms. 
        /// Otherwise - only to the room he is currently in
        /// </summary>
        bool ShouldListenAllRooms { get; }
        Action<R> this[E eventType] { get; }
    }
}

