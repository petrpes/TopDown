using System;
using System.Collections.Generic;

public class RoomEventHandler : IRoomEventHandler
{
    public static RoomEventHandler Instance = new RoomEventHandler();

    private Dictionary<IRoom, Dictionary<RoomEventType, Action>> _events;
    private Dictionary<RoomEventType, Action<IRoom>> _allRoomsEvents;

    private int RoomEventTypeCount = Enum.GetValues(typeof(RoomEventType)).Length;

#region dictionaries
    private Action this[IRoom room, RoomEventType eventType]
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
                _events = new Dictionary<IRoom, Dictionary<RoomEventType, Action>>();
            }
            if (!_events.ContainsKey(room))
            {
                _events.Add(room, new Dictionary<RoomEventType, Action>(RoomEventTypeCount));
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
        if (this[room, eventType] != null)
        {
            this[room, eventType].Invoke();
        }
        if (this[eventType] != null)
        {
            this[eventType].Invoke(room);
        }
    }

    public void SubscribeListener(IRoomEventListener listener, IRoom room)
    {
        foreach (RoomEventType eventType in Enum.GetValues(typeof(RoomEventType)))
        {
            if (listener[eventType] != null)
            {
                this[room, eventType] += listener[eventType];
            }
        }
    }

    public void UnsubscribeListener(IRoomEventListener listener, IRoom room)
    {
        foreach (RoomEventType eventType in Enum.GetValues(typeof(RoomEventType)))
        {
            if (listener[eventType] != null)
            {
                this[room, eventType] -= listener[eventType];
            }
        }
    }

    public void SubscribeListener(IAllRoomsEventListener listener)
    {
        foreach (RoomEventType eventType in Enum.GetValues(typeof(RoomEventType)))
        {
            if (listener[eventType] != null)
            {
                this[eventType] += listener[eventType];
            }
        }
    }

    public void UnsubscribeListener(IAllRoomsEventListener listener)
    {
        foreach (RoomEventType eventType in Enum.GetValues(typeof(RoomEventType)))
        {
            if (listener[eventType] != null)
            {
                this[eventType] -= listener[eventType];
            }
        }
    }

    public void Dispose()
    {
        _events = null;
    }
}

