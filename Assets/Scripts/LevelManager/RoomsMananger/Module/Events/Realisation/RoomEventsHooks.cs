using System;
using System.Collections.Generic;

namespace Components.RoomsManager
{
    public class RoomEventsHooks<E, R> : IRoomEventsHooks<E, R> where R : class where E : struct, IConvertible
    {
        private Dictionary<R, RoomEventsDictionary> _actions;
        private RoomEventsDictionary _action;

        public RoomEventsHooks(int maxRoomsCount = 10)
        {
            _action = new RoomEventsDictionary();
            _actions = new Dictionary<R, RoomEventsDictionary>(maxRoomsCount);
        }

        private Action<R> this[E eventType]
        {
            get
            {
                return _action[eventType];
            }

            set
            {
                _action[eventType] = value;
            }
        }

        private Action<R> this[R room, E eventType]
        {
            get
            {
                if (!_actions.ContainsKey(room))
                {
                    _actions[room] = new RoomEventsDictionary();
                }
                return _actions[room][eventType];
            }
            set
            {
                if (!_actions.ContainsKey(room))
                {
                    _actions[room] = new RoomEventsDictionary();
                }
                _actions[room][eventType] = value;
            }
        }

        public void Dispose()
        {
            _action = null;
            _actions.Clear();
        }

        public void Invoke(R room, E eventType)
        {
            if (this[room, eventType] != null)
            {
                this[room, eventType](room);
            }
            if (this[eventType] != null)
            {
                this[eventType](room);
            }
        }

        public void Subscribe(E eventType, Action<R> action, R room = null)
        {
            if (room == null)
            {
                this[eventType] += action;
            }
            else
            {
                this[room, eventType] += action;
            }
        }

        public void UnSubscribe(E eventType, Action<R> action, R room = null)
        {
            if (room == null)
            {
                this[eventType] -= action;
            }
            else
            {
                this[room, eventType] -= action;
            }
        }

        private class RoomEventsDictionary : EnumActionDictionary<E, R>
        {
        }
    }
}

