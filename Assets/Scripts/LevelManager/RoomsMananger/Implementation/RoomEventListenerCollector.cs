using Components.RoomsManager;
using System;
using System.Collections.Generic;

public class RoomEventListenerCollector : IPublicRoomEventListener, IRoomEventListener
{
    private EnumActionDictionary<RoomEventType, IRoom> _actions;
    public bool ShouldListenAllRooms { get; private set; }

    public RoomEventListenerCollector(bool listenAllRooms, IEnumerable<IRoomEventListener> listeners)
    {
        ShouldListenAllRooms = listenAllRooms;

        _actions = new EnumActionDictionary<RoomEventType, IRoom>();

        foreach (var listener in listeners)
        {
            foreach (var key in StructsExtentions.GetEnumValues<RoomEventType>())
            {
                if (listener[key] != null)
                {
                    _actions[key] += listener[key];
                }
            }
        }
    }

    public Action<IRoom> this[RoomEventType eventType]
    {
        get
        {
            return _actions[eventType];
        }
    }

    public IRoomEventListener Listener
    {
        get
        {
            return this;
        }
    }
}

