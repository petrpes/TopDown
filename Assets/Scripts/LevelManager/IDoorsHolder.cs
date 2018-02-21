using System;
using System.Collections.Generic;

public interface IDoorsHolder
{
    IEnumerable<IDoor> GetDoors(Func<IDoor, bool> predicate = null);
}

