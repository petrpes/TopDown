using System;

namespace Components.RoomsManager
{
    public class RoomsManager<E, R, O> where R : class where E : struct, IConvertible
    {
        /// <summary>
        /// Public rooms events. Any action might be subscribed to them.
        /// </summary>
        public readonly IRoomEventsHooks<E, R> Hooks;
        /// <summary>
        /// Subscribes objects of type IRoomEventListener to room events
        /// </summary>
        public readonly IRoomEventsSubscriber<E, R> EventsSubscriber;
        /// <summary>
        /// Room content mananger
        /// </summary>
        public readonly IRoomContent<O, R> Content;
        /// <summary>
        /// Handler of rooms' transitions. This will subscribe asynchronous porocesses that 
        /// will happen when rooms changed
        /// </summary>
        public readonly IRoomTransitionHandler<R> TransitionHandler;
        /// <summary>
        /// Use this API when you need to change the current room
        /// </summary>
        public readonly IRoomsChanger<R> RoomsChanger;

        private IRoomsHooksMediator<E, R, O>[] _mediators;

        /// <summary>
        /// Represents APIs of rooms' management
        /// </summary>
        /// <param name="transitionHandler">This will subscribe asynchronous porocesses that will happen when rooms changed</param>
        /// <param name="hooks">Public rooms events. Any action might be subscribed to them.</param>
        /// <param name="content">Room content mananger</param>
        /// <param name="roomsChanger">Use this API when you need to change the current room</param>
        /// <param name="subscriber">Subscribes objects of type IRoomEventListener to room events</param>
        /// <param name="mediators">Classes which should connect APIs' events whith each other</param>
        public RoomsManager(IRoomTransitionHandler<R> transitionHandler = null,
                            IRoomEventsHooks<E, R> hooks = null,
                            IRoomContent<O, R> content = null,
                            IRoomsChanger<R> roomsChanger = null,
                            IRoomEventsSubscriber<E, R> subscriber = null,
                            params IRoomsHooksMediator<E, R, O>[] mediators)
        {
            TransitionHandler = transitionHandler;
            Hooks = hooks ?? new RoomEventsHooks<E, R>();
            Content = content ?? new RoomContentList<O, R>();
            RoomsChanger = roomsChanger ?? new RoomChanger<R>(TransitionHandler);
            EventsSubscriber = subscriber ?? new RoomEventsSubscriber<E, R>(Hooks);

            _mediators = mediators;
            foreach (var mediator in _mediators)
            {
                mediator.Connect(this);
            }
        }

        public void Dispose()
        {
            Hooks.Dispose();
            Content.Dispose();
            TransitionHandler.ForceStop();
            RoomsChanger.Dispose();
        }
    }
}

