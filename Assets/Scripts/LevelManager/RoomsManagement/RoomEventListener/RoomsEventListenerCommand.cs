using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomsEventListenerCommand : MonoBehaviour, IRoomEventListener
{
    [SerializeField] private Command[] _onOpenCommands;
    [SerializeField] private Command[] _onCloseCommands;
    [SerializeField] private Command[] _onClearCommands;
    [SerializeField] private Command[] _onStartCommands;

    private Dictionary<RoomEventType, Action<IRoom>> _actions;

    public Action<IRoom> this[RoomEventType eventType]
    {
        get
        {
            if (_actions == null)
            {
                _actions = new Dictionary<RoomEventType, Action<IRoom>>(Enum.GetValues(typeof(RoomEventType)).Length);

                AddCommand(RoomEventType.OnAfterOpen, _onOpenCommands);
                AddCommand(RoomEventType.OnBeforeClose, _onCloseCommands);
                AddCommand(RoomEventType.OnClear, _onClearCommands);
                AddCommand(RoomEventType.OnStarted, _onStartCommands);
            }
            return _actions.ContainsKey(eventType) ? _actions[eventType] : null;
        }
    }

    private void AddCommand(RoomEventType eventType, Command[] commands)
    {
        if (commands == null || commands.Length > 0)
        {
            return;
        }
        _actions.Add(eventType, (room) => 
        {
            if (isActiveAndEnabled)
            {
                commands.Execute(gameObject);
            }
        });
    }
}

