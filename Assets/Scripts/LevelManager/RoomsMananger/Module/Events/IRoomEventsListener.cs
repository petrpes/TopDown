using System;

namespace Components.RoomsManager
{
    public interface IRoomEventListener<E, R> where E : struct, IConvertible
    {
        Action<R> this[E eventType] { get; }
    }
}

