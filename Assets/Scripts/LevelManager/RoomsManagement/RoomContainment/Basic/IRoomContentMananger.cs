using System;
using System.Collections.Generic;

public interface IRoomContentMananger : IDisposable
{
    void AddObject(IRoom room, IRoomContainedObject roomObject);
    void RemoveObject(IRoom room, IRoomContainedObject roomObject);
    IEnumerable<IRoomContainedObject> GetObjects(IRoom room, Func<IRoomContainedObject, bool> predicate = null);
    int GetObjectsCount(IRoom room);

    event Action<IRoom, IRoomContainedObject> ObjectAdded;
    event Action<IRoom, IRoomContainedObject> ObjectRemoved;
}

