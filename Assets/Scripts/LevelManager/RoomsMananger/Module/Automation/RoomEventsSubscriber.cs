using System;

namespace Components.RoomsManager
{
    public class RoomEventsSubscriber<E, R> : IRoomEventsSubscriber<E, R> where E : struct, IConvertible where R : class
    {
        private IRoomEventsHooks<E, R> _roomEventHooks;

        public RoomEventsSubscriber(IRoomEventsHooks<E, R> roomEventHooks)
        {
            _roomEventHooks = roomEventHooks;
        }

        public void Dispose()
        {
        }

        public void SubscribeListener(IRoomEventListener<E, R> listener, R room)
        {
            foreach (E eventType in Enum.GetValues(typeof(E)))
            {
                if (listener[eventType] != null)
                {
                    _roomEventHooks.Subscribe(eventType, listener[eventType], room);
                }
            }
        }

        public void UnsubscribeListener(IRoomEventListener<E, R> listener, R room)
        {
            foreach (E eventType in Enum.GetValues(typeof(E)))
            {
                if (listener[eventType] != null)
                {
                    _roomEventHooks.UnSubscribe(eventType, listener[eventType], room);
                }
            }
        }
    }
}

