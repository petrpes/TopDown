using Components.RoomsManager;
using System;
using UnityEngine;

public class RoomEventListenerProxy : MonoBehaviour, IRoomEventListener
{
    [SerializeField] private bool _shouldListenAllRooms;
    [SerializeField] private ComponentsCache _roomEventsListeners = 
        new ComponentsCache(typeof(IRoomEventListener), true);

    public bool ShouldListenAllRooms { get { return _shouldListenAllRooms; } }

    private EnumActionDictionary<RoomEventType, IRoom> _actions;

    public Action<IRoom> this[RoomEventType eventType]
    {
        get
        {
            if (isActiveAndEnabled)
            {
                if (_actions == null)
                {
                    _actions = new EnumActionDictionary<RoomEventType, IRoom>();
                    foreach (var listener in _roomEventsListeners.GetCachedComponets<IRoomEventListener>())
                    {
                        foreach (var roomEvent in StructsExtentions.GetEnumValues<RoomEventType>())
                        {
                            if (listener[roomEvent] != null)
                            {
                                _actions[roomEvent] += listener[roomEvent];
                            }
                        }
                    }
                }

                return _actions[eventType];
            }
            return null;
        }
    }
}

