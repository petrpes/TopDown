using System;

namespace Components.RoomsManager
{
    /// <summary>
    /// A class that will subscribe a listeners to room events
    /// </summary>
    /// <typeparam name="E">Enum for event type</typeparam>
    /// <typeparam name="R">Room</typeparam>
    public interface IRoomEventsSubscriber<E, R> : IDisposable where E : struct, IConvertible
    {
        void SubscribeListener(IRoomEventListener<E, R> listener, R room);
        void UnsubscribeListener(IRoomEventListener<E, R> listener, R room);
    }
}

