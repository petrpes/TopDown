using System;
using System.Collections.Generic;

public class RoomEventHandler : IRoomEventHandler
{
    public static RoomEventHandler Instance = new RoomEventHandler();
    private int RoomEventTypeCount = Enum.GetValues(typeof(RoomEventType)).Length;

#region dictionaries

    private Dictionary<IRoom, Dictionary<RoomEventType, Action<IRoom>>> _events;
    private Dictionary<RoomEventType, Action<IRoom>> _allRoomsEvents;

    private Action<IRoom> this[IRoom room, RoomEventType eventType]
    {
        get
        {
            if (_events != null && 
                _events.ContainsKey(room) && 
                _events[room] != null &&
                _events[room].ContainsKey(eventType) &&
                _events[room][eventType] != null)
            {
                return _events[room][eventType];
            }

            return null;
        }
        set
        {
            if (_events == null)
            {
                _events = new Dictionary<IRoom, Dictionary<RoomEventType, Action<IRoom>>>();
            }
            if (!_events.ContainsKey(room))
            {
                _events.Add(room, new Dictionary<RoomEventType, Action<IRoom>>(RoomEventTypeCount));
            }
            if (!_events[room].ContainsKey(eventType))
            {
                _events[room].Add(eventType, value);
            }
            else
            {
                _events[room][eventType] = value;
            }
        }
    }

    private Action<IRoom> this[RoomEventType eventType]
    {
        get
        {
            if (_allRoomsEvents != null &&
                _allRoomsEvents.ContainsKey(eventType) &&
                _allRoomsEvents[eventType] != null)
            {
                return _allRoomsEvents[eventType];
            }

            return null;
        }
        set
        {
            if (_allRoomsEvents == null)
            {
                _allRoomsEvents = new Dictionary<RoomEventType, Action<IRoom>>(RoomEventTypeCount);
            }
            if (!_allRoomsEvents.ContainsKey(eventType))
            {
                _allRoomsEvents.Add(eventType, value);
            }
            else
            {
                _allRoomsEvents[eventType] = value;
            }
        }
    }
#endregion

    public void InvokeRoomEvent(IRoom room, RoomEventType eventType)
    {
        this[room, eventType].SafeInvoke(room);
        this[eventType].SafeInvoke(room);
    }

    public void SubscribeListener(IRoomEventListener listener, IRoom room)
    {
        foreach (RoomEventType eventType in Enum.GetValues(typeof(RoomEventType)))
        {
            if (listener[eventType] != null)
            {
                if (room == null)
                {
                    this[eventType] += listener[eventType];
                }
                else
                {
                    this[room, eventType] += listener[eventType];
                }
            }
        }
    }

    public void UnsubscribeListener(IRoomEventListener listener, IRoom room)
    {
        foreach (RoomEventType eventType in Enum.GetValues(typeof(RoomEventType)))
        {
            if (listener[eventType] != null)
            {
                if (room == null)
                {
                    this[eventType] -= listener[eventType];
                }
                else
                {
                    this[room, eventType] -= listener[eventType];
                }
            }
        }
    }
    
    public void Dispose()
    {
        _events = null;
    }
}

