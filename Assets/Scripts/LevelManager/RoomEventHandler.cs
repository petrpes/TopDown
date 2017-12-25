using System.Collections.Generic;
using UnityEngine;

public class RoomEventHandler
{
    public static RoomEventHandler Instance = new RoomEventHandler();

    private Dictionary<IRoom, InvokeableEvent<RoomEventType>> _events;
    private InvokeableEvent<IRoom, RoomEventType> _allRoomsEvents;

    public RoomEventHandler()
    {
        _events = new Dictionary<IRoom, InvokeableEvent<RoomEventType>>();
        _allRoomsEvents = new InvokeableEvent<IRoom, RoomEventType>();
    }

    public void SubscribeListener(IRoom room, IRoomEventListener eventListener)
    {
        if (!_events.ContainsKey(room))
        {
            _events.Add(room, new InvokeableEvent<RoomEventType>());
        }
        _events[room].Event += eventListener.OnRoomEvent;
    }

    public void UnsubscribeListener(IRoom room, IRoomEventListener eventListener)
    {
        if (!_events.ContainsKey(room))
        {
            Debug.LogError("Cannot unsubscribe - room is not present in a dictionary");
            return;
        }
        _events[room].Event -= eventListener.OnRoomEvent;

        if (_events[room].IsEmpty)
        {
            _events.Remove(room);
        }
    }

    public void SubscribeListener(IAllRoomsEventListener eventListener)
    {
        _allRoomsEvents.Event += eventListener.OnRoomEvent;
    }

    public void UnsubscribeListener(IAllRoomsEventListener eventListener)
    {
        _allRoomsEvents.Event -= eventListener.OnRoomEvent;
    }

    public void InvokeEvent(IRoom room, RoomEventType eventType)
    {
        _allRoomsEvents.InvokeEvent(room, eventType);

        if (!_events.ContainsKey(room))
        {
            return;
        }
        _events[room].InvokeEvent(eventType);
    }
}

public interface IRoomEventListener
{
    void OnRoomEvent(RoomEventType eventType);
}

public interface IAllRoomsEventListener
{
    void OnRoomEvent(IRoom room, RoomEventType eventType);
}

