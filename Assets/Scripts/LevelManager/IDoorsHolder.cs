using System;
using System.Collections.Generic;

public interface IDoorsHolder
{
    IEnumerator<IDoor> GetDoors(Func<IDoor, bool> predicate);
}

