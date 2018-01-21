using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomEventListenerProxy : MonoBehaviour, IRoomEventListener
{
    [SerializeField] private ComponentsCache _roomEventsListener = 
        new ComponentsCache(typeof(IRoomEventListener).Name, true);
    [SerializeField] private RoomContainedObject _roomObject;

    private Dictionary<RoomEventType, Action<IRoom>> _actions;

    private void FillActionsDictionary()
    {
        _actions = new Dictionary<RoomEventType, Action<IRoom>>(Enum.GetValues(typeof(RoomEventType)).Length);

        foreach (RoomEventType eventType in Enum.GetValues(typeof(RoomEventType)))
        {
            _actions.Add(eventType, null);

            foreach (var eventListeners in
                _roomEventsListener.GetCachedComponets<IRoomEventListener>())
            {
                if (eventListeners[eventType] != null)
                {
                    _actions[eventType] += eventListeners[eventType];
                }
            }
        }
    }

    private void Awake()
    {
        FillActionsDictionary();
        SubscribeToChangeRoom();
    }

    private void OnDestroy()
    {
        UnsubscribeFromChangeRoom();
    }

    public Action<IRoom> this[RoomEventType eventType]
    {
        get
        {
            return _actions[eventType];
        }
    }

    private void OnRoomChanged(IRoom roomBefore, IRoom roomAfter)
    {
        if (roomBefore != null)
        {
            UnsubscribeFromRoomEventHandler(roomBefore);
        }
        if (roomAfter != null)
        {
            SubscribeToRoomEventHandler(roomBefore);
        }
    }

    private void SubscribeToChangeRoom()
    {
        if (_roomObject == null)
        {
            SubscribeToRoomEventHandler(null);
            return;
        }
        _roomObject.RoomChanged += OnRoomChanged;
    }

    private void UnsubscribeFromChangeRoom()
    {
        if (_roomObject == null)
        {
            UnsubscribeFromRoomEventHandler(null);
            return;
        }
        _roomObject.RoomChanged -= OnRoomChanged;
    }

    private void SubscribeToRoomEventHandler(IRoom room)
    {
        RoomEventHandler.Instance.SubscribeListener(this, room);
    }

    private void UnsubscribeFromRoomEventHandler(IRoom room)
    {
        RoomEventHandler.Instance.UnsubscribeListener(this, room);
    }
}

