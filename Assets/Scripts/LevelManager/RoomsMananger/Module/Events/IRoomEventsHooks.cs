using System;

namespace Components.RoomsManager
{
    /// <summary>
    /// An interface for subscribing actions to Rooms' events
    /// </summary>
    /// <typeparam name="E">Enum type for room events</typeparam>
    /// <typeparam name="R">Room</typeparam>
    public interface IRoomEventsHooks<E, R> : IDisposable where R : class
    {
        /// <summary>
        /// Subscribe action to room events
        /// </summary>
        /// <param name="eventType">Event type</param>
        /// <param name="action">Action to subscribe</param>
        /// <param name="room">If set - subscribe action to the selected room's events. Otherwise - to all rooms' events</param>
        void Subscribe(E eventType, Action<R> action, R room = null);

        /// <summary>
        /// Unsubscribe action from room events
        /// </summary>
        /// <param name="eventType">Event type</param>
        /// <param name="action">Action to unsubscribe</param>
        /// <param name="room">If set - unsubscribe action from the selected room's events. Otherwise - from all rooms' events</param>
        void UnSubscribe(E eventType, Action<R> action, R room = null);

        /// <summary>
        /// Invokes selected event
        /// </summary>
        /// <param name="room">Room, in which event is happening</param>
        /// <param name="eventType">Event type</param>
        void Invoke(R room, E eventType);
    }
}

