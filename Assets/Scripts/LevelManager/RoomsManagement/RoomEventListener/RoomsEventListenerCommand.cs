using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomsEventListenerCommand : MonoBehaviour, IRoomEventListener
{
    [SerializeField] private Command[] _onOpenCommands;
    [SerializeField] private Command[] _onCloseCommands;
    [SerializeField] private Command[] _onClearCommands;
    [SerializeField] private Command[] _onStartCommands;
    [SerializeField] private bool _shouldDebug;

    private Dictionary<RoomEventType, Action<IRoom>> _actions;
    [NonSerialized] private bool _isActionsCreated = false;

    public Action<IRoom> this[RoomEventType eventType]
    {
        get
        {
            if (!_isActionsCreated)
            {
                _actions = new Dictionary<RoomEventType, Action<IRoom>>
                    (RoomEventType.OnAfterOpen.EnumLength());

                AddCommand(RoomEventType.OnAfterOpen, _onOpenCommands);
                AddCommand(RoomEventType.OnBeforeClose, _onCloseCommands);
                AddCommand(RoomEventType.OnClear, _onClearCommands);
                AddCommand(RoomEventType.OnStarted, _onStartCommands);

                _isActionsCreated = true;
            }
            return _actions.SafeGetValue(eventType);
        }
    }

    private void AddCommand(RoomEventType eventType, Command[] commands)
    {
        if (commands == null || commands.Length <= 0)
        {
            return;
        }
        _actions.Add(eventType, (room) => 
        {
            if (isActiveAndEnabled)
            {
                Log(eventType);
                commands.Execute(gameObject);
            }
        });
    }

    private void Log(RoomEventType eventType)
    {
        if (_shouldDebug)
        {
            Debug.Log(gameObject.name + " " + eventType);
        }
    }
}

