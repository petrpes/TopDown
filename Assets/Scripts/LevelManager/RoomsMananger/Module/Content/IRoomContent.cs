using System;
using System.Collections.Generic;

namespace Components.RoomsManager
{
    public interface IRoomContent<O, R> : IDisposable
    {
        void AddObject(R room, O roomObject);
        void RemoveObject(R room, O roomObject);
        IEnumerable<O> GetContent(R room, Predicate<O> predicate);

        event Action<O, R> OnObjectAddedToTheRoom;
        event Action<O, R> OnObjectRemovedFromRoom;
    }
}

