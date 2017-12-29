using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RoomContainedObject))]
public class RoomEventListenerProxy : MonoBehaviour, IRoomEventListener
{
    [SerializeField] private ComponentsCache _roomEventsListener = 
        new ComponentsCache(typeof(IRoomEventListener).Name, true);
    [SerializeField] private RoomContainedObject _roomObject;

    private Dictionary<RoomEventType, Action> _actions;

    private void FillActionsDictionary()
    {
        _actions = new Dictionary<RoomEventType, Action>(Enum.GetValues(typeof(RoomEventType)).Length);

        foreach (RoomEventType eventType in Enum.GetValues(typeof(RoomEventType)))
        {
            _actions.Add(eventType, null);

            foreach (var eventListeners in
                _roomEventsListener.GetCachedComponets<IRoomEventListener>())
            {
                if (!eventListeners.Equals(this))
                {
                    _actions[eventType] += eventListeners[eventType];
                }
            }
        }
    }

    private void Awake()
    {
        FillActionsDictionary();

        _roomObject.OnObjectAppearInRoom += Subscribe;
        _roomObject.OnObjectDisappearFromRoom += Unsubscribe;

        if (_roomObject.CurrentRoom != null)
        {
            Subscribe(_roomObject.CurrentRoom);
        }
    }

    private void OnDestroy()
    {
        _roomObject.OnObjectAppearInRoom -= Subscribe;
        _roomObject.OnObjectDisappearFromRoom -= Unsubscribe;
    }

    public Action this[RoomEventType eventType]
    {
        get
        {
            return _actions[eventType];
        }
    }

    private void Subscribe(IRoom room)
    {
        RoomEventHandler.Instance.SubscribeListener(this, room);
    }

    private void Unsubscribe(IRoom room)
    {
        RoomEventHandler.Instance.UnsubscribeListener(this, room);
    }
}

