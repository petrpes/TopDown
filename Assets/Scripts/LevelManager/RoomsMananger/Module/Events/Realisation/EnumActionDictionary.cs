using System;
using System.Collections.Generic;

namespace Components.RoomsManager
{
    public class EnumActionDictionary<E, A> where E : struct, IConvertible
    {
        private Dictionary<E, Action<A>> _dictionary;

        public EnumActionDictionary()
        {
            var eventsCount = Enum.GetValues(typeof(E)).Length;

            _dictionary = new Dictionary<E, Action<A>>(eventsCount);
            foreach (E eventType in Enum.GetValues(typeof(E)))
            {
                _dictionary[eventType] = null;
            }
        }

        public void Clear()
        {
            foreach (E eventType in Enum.GetValues(typeof(E)))
            {
                _dictionary[eventType] = null;
            }
        }

        public Action<A> this[E eventType]
        {
            get
            {
                return _dictionary[eventType];
            }
            set
            {
                _dictionary[eventType] = value;
            }
        }
    }
}

