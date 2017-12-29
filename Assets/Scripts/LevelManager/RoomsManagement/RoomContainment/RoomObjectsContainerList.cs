using System;
using System.Collections.Generic;

public class RoomObjectsContainerList : IRoomObjectsContainer
{
    private List<IRoomContainedObject> _list;
    private const int StartCount = 10;

    public RoomObjectsContainerList()
    {
        _list = new List<IRoomContainedObject>(StartCount);
    }

    public int Count { get { return _list.Count; } }

    public void AddObject(IRoomContainedObject roomObject)
    {
        _list.Add(roomObject);
    }

    public void Dispose()
    {
        _list = new List<IRoomContainedObject>(StartCount);
    }

    public IEnumerable<IRoomContainedObject> GetObjects(Func<IRoomContainedObject, bool> predicate = null)
    {
        for (int i = 0; i < Count; i++)
        {
            yield return _list[i];
        }
    }

    public void RemoveObject(IRoomContainedObject roomObject)
    {
        _list.Remove(roomObject);
    }
}

