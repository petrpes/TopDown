using System;
using System.Collections.Generic;

namespace Components.RoomsManager
{
    public class RoomContentList<O, R> : IRoomContent<O, R>
    {
        private Dictionary<R, IList<O>> _objectsDictionary;

        public event Action<O, R> OnObjectAddedToTheRoom;
        public event Action<O, R> OnObjectRemovedFromRoom;

        public RoomContentList(int maxRoomsCount = 10)
        {
            _objectsDictionary = new Dictionary<R, IList<O>>(maxRoomsCount);
        }

        public void AddObject(R room, O roomObject)
        {
            if (!_objectsDictionary.ContainsKey(room))
            {
                _objectsDictionary[room] = new List<O>(10);
            }
            _objectsDictionary[room].Add(roomObject);

            if (OnObjectAddedToTheRoom != null)
            {
                OnObjectAddedToTheRoom.Invoke(roomObject, room);
            }
        }

        public void RemoveObject(R room, O roomObject)
        {
            if (!_objectsDictionary.ContainsKey(room))
            {
                return;
            }
            _objectsDictionary[room].Remove(roomObject);

            if (OnObjectRemovedFromRoom != null)
            {
                OnObjectRemovedFromRoom.Invoke(roomObject, room);
            }
        }

        public IEnumerable<O> GetContent(R room, Predicate<O> predicate)
        {
            foreach (var roomObject in _objectsDictionary[room])
            {
                if (predicate == null || predicate(roomObject))
                {
                    yield return roomObject;
                }
            }
        }

        public void Dispose()
        {
            _objectsDictionary.Clear();
        }
    }
}

