using System;
using System.Collections.Generic;

public interface IRoomObjectsContainer : IDisposable
{
    void AddObject(IRoomContainedObject roomObject);
    void RemoveObject(IRoomContainedObject roomObject);
    IEnumerable<IRoomContainedObject> GetObjects(Func<IRoomContainedObject, bool> predicate = null);
    int Count { get; }
}

