using System;
using UnityEngine;
using Components.RoomsManager;

public class RoomEventListenerCommands : MonoBehaviour, IRoomEventListener
{
    [SerializeField] private Command[] _onOpenCommands;
    [SerializeField] private Command[] _onCloseCommands;

    [NonSerialized] private bool _onActionsCreated = false;
    private EnumActionDictionary<RoomEventType, IRoom> _dictionary;

    public bool ShouldListenAllRooms { get { return false; } }

    public Action<IRoom> this[RoomEventType eventType]
    {
        get
        {
            CreateDictionary();
            return _dictionary[eventType];
        }
    }

    private void CreateDictionary()
    {
        if (!_onActionsCreated)
        {
            _dictionary = new EnumActionDictionary<RoomEventType, IRoom>();
            FillDictionary(RoomEventType.OnOpen, _onOpenCommands);
            FillDictionary(RoomEventType.OnClosed, _onCloseCommands);
            _onActionsCreated = true;
        }
    }

    private void FillDictionary(RoomEventType eventType, Command[] array)
    {
        foreach (var command in array)
        {
            _dictionary[eventType] += (room) => command.Execute(gameObject);
        }
    }
}

