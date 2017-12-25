using System;
using System.Collections.Generic;

public interface ILevelMap
{
    IEnumerable<IRoom> GetRooms<T>(Func<T, bool> predicate) where T : IRoomsPredicateArguments;
    IEnumerable<IRoom> GetRooms();
}

public interface IRoomsPredicateArguments
{
}

