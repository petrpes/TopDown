using System;

namespace Components.RoomsManager
{
    /// <summary>
    /// This class will automatically subscribe listeners to rooms' events when it will be 
    /// added to any room's content
    /// </summary>
    /// <typeparam name="E">Enum for room events type</typeparam>
    /// <typeparam name="R">Room type</typeparam>
    /// <typeparam name="O">Objects type</typeparam>
    public class RoomsHooksMediator<E, R, O> : IRoomsHooksMediator<E, R, O> where R : class where E : struct, IConvertible
    {
        private RoomsManager<E, R, O> _manager;

        public virtual void Connect(RoomsManager<E, R, O> manager)
        {
            _manager = manager;

            manager.Content.OnObjectAddedToTheRoom += ObjectAddedToTheRoom;
            manager.Content.OnObjectRemovedFromRoom += ObjectRemovedFromRoom;
        }

        private void ObjectAddedToTheRoom(O roomObject, R room)
        {
            var listener = RetrieveListener(roomObject);

            if (listener != null && !ShouldListenAllRooms(roomObject))
            {
                _manager.EventsSubscriber.SubscribeListener(listener, room);
            }
        }

        private void ObjectRemovedFromRoom(O roomObject, R room)
        {
            var listener = RetrieveListener(roomObject);

            if (listener != null && !ShouldListenAllRooms(roomObject))
            {
                _manager.EventsSubscriber.UnsubscribeListener(listener, room);
            }
        }

        protected virtual IRoomEventListener<E, R> RetrieveListener(O roomObject)
        {
            return roomObject as IRoomEventListener<E, R>;
        }

        protected virtual bool ShouldListenAllRooms(O roomObject)
        {
            return true;
        }
    }
}

