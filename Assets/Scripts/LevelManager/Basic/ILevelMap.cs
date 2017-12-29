using System;
using System.Collections.Generic;

public interface ILevelMap
{
    IEnumerable<IRoom> GetRooms(Func<IRoom, bool> predicate = null);
    int RoomsCount { get; }
}

