using System;
using System.Collections.Generic;

public class RoomContentManangerDictionary : IRoomContentMananger
{
    private Dictionary<IRoom, IRoomObjectsContainer> _dictionary;
    private const int StartCapacity = 10;

    public event Action<IRoom, IRoomContainedObject> ObjectAdded;
    public event Action<IRoom, IRoomContainedObject> ObjectRemoved;

    public RoomContentManangerDictionary()
    {
        Dispose();
    }

    private IRoomObjectsContainer this[IRoom room]
    {
        get
        {
            if (_dictionary.ContainsKey(room))
            {
                if (_dictionary[room] == null)
                {
                    _dictionary[room] = new RoomObjectsContainerList();
                }
                return _dictionary[room];
            }
            _dictionary.Add(room, new RoomObjectsContainerList());
            return _dictionary[room];
        }
    }

    public void Dispose()
    {
        _dictionary = new Dictionary<IRoom, IRoomObjectsContainer>(StartCapacity);
    }

    public void AddObject(IRoom room, IRoomContainedObject roomObject)
    {
        this[room].AddObject(roomObject);
        ObjectAdded.SafeInvoke(room, roomObject);
    }

    public void RemoveObject(IRoom room, IRoomContainedObject roomObject)
    {
        this[room].RemoveObject(roomObject);
        ObjectRemoved.SafeInvoke(room, roomObject);
    }

    public IEnumerable<IRoomContainedObject> GetObjects(IRoom room, Func<IRoomContainedObject, bool> predicate = null)
    {
        return this[room].GetObjects();
    }

    public int GetObjectsCount(IRoom room)
    {
        return this[room].Count;
    }
}

