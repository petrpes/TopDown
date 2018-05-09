using System;
using System.Collections;
using System.Collections.Generic;
public class LevelEventListenerCollection : ILevelEventListener
{
    private Action<ILevel> _onCreated;
    private Action<ILevel> _onStarted;
    private Action<ILevel> _onDestroyed;

    public LevelEventListenerCollection(IEnumerable<ILevelEventListener> collection)
    {
        foreach (var listener in collection)
        {
            _onCreated += listener.OnLevelCreated;
            _onStarted += listener.OnLevelStarted;
            _onDestroyed += listener.OnLevelDestroyed;
        }
    }

    public Action<ILevel> OnLevelCreated
    {
        get
        {
            return _onCreated;
        }
    }

    public Action<ILevel> OnLevelStarted
    {
        get
        {
            return _onStarted;
        }
    }

    public Action<ILevel> OnLevelDestroyed
    {
        get
        {
            return _onDestroyed;
        }
    }
}

