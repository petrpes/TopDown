using System;

namespace Components.RoomsManager
{
    public interface IRoomsHooksMediator<E, R, O> where R : class where E : struct, IConvertible
    {
        void Connect(RoomsManager<E, R, O> manager);
    }
}

