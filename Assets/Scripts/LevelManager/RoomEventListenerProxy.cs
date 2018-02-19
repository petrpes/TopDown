using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomEventListenerProxy : MonoBehaviour, IRoomEventListener
{
    [SerializeField] private ComponentsCache _roomEventsListener = 
        new ComponentsCache(typeof(IRoomEventListener).Name, true);
    [SerializeField] private RoomContainedObject _roomObject;

    private Dictionary<RoomEventType, Action<IRoom>> _actions;

    public Action<IRoom> this[RoomEventType eventType]
    {
        get
        {
            if (isActiveAndEnabled)
            {
                return _actions[eventType];
            }
            return null;
        }
    }

    private void Awake()
    {
        FillActionsDictionary();
        SubscribeToEvents();
    }
    
    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

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

    private void SubscribeToEvents()
    {
        if (_roomObject == null)
        {
            SubscribeToRoomEventHandler(null);
        }
        else
        {
            if (_roomObject.CurrentRoom != null)
            {
                SubscribeToRoomEventHandler(_roomObject.CurrentRoom);
            }
            _roomObject.RoomChanged += OnRoomChanged;
        }
    }

    private void UnsubscribeFromEvents()
    {
        if (_roomObject == null)
        {
            UnsubscribeFromRoomEventHandler(null);
        }
        else
        {
            if (_roomObject.CurrentRoom != null)
            {
                UnsubscribeFromRoomEventHandler(_roomObject.CurrentRoom);
            }
            _roomObject.RoomChanged -= OnRoomChanged;
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

    private void SubscribeToRoomEventHandler(IRoom room)
    {
        RoomEventHandler.Instance.SubscribeListener(this, room);
    }

    private void UnsubscribeFromRoomEventHandler(IRoom room)
    {
        RoomEventHandler.Instance.UnsubscribeListener(this, room);
    }
}

