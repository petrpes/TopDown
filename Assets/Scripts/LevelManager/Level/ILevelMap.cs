using System;
using System.Collections.Generic;

public interface ILevelMap
{
    IEnumerable<IRoom> GetRooms(Predicate<IRoom> predicate = null);
    int RoomsCount { get; }
}

