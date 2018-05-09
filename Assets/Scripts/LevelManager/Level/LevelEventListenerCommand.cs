using System;
using UnityEngine;

public class LevelEventListenerCommand : MonoBehaviour, ILevelEventListener
{
    [SerializeField] private Command[] _onCreatedCommands;
    [SerializeField] private Command[] _onStartCommands;
    [SerializeField] private Command[] _onDestroyedCommands;

    [SerializeField] private GameObject _parent;

    public bool ShouldListenAllRooms { get { return false; } }

    public Action<ILevel> OnLevelCreated
    {
        get
        {
            return (level) => { Execute(_onCreatedCommands); };
        }
    }

    public Action<ILevel> OnLevelStarted
    {
        get
        {
            return (level) => { Execute(_onStartCommands); };
        }
    }

    public Action<ILevel> OnLevelDestroyed
    {
        get
        {
            return (level) => { Execute(_onDestroyedCommands); };
        }
    }

    private void Execute(Command[] array)
    {
        foreach (var command in array)
        {
            command.Execute(_parent);
        }
    }
}
