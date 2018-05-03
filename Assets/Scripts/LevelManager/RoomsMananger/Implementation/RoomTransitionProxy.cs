using Components.RoomsManager;
using System;
using System.Collections.Generic;

public class RoomTransitionProxy<R> : IRoomTransitionHandler<R>
{
    private CallbackCollector _callbackCollector;
    private Dictionary<IRoomTransition<R>, Action<bool>> _transitionsDictionary;
    private bool _isPaused = false;

    public bool IsInProcess { get { return _callbackCollector.IsRunning; } }

    public RoomTransitionProxy()
    {
        _callbackCollector = new CallbackCollector();

        _transitionsDictionary = new Dictionary<IRoomTransition<R>, Action<bool>>();
    }

    public void SubscribeTransistor(IRoomTransition<R> transitionObject, Action<bool> onComplete)
    {
        _transitionsDictionary.Add(transitionObject, onComplete);
    }

    public void UnSubscribeTransistor(IRoomTransition<R> transitionObject)
    {
        _transitionsDictionary.Remove(transitionObject);
    }

    public void InvokeTransitionAction(R oldRoom, R newRoom, Action onComplete)
    {
        foreach (var transitionObject in _transitionsDictionary.Keys)
        {
            transitionObject.InvokeTransitionAction(oldRoom, newRoom, _callbackCollector.AddCallback());
        }

        _callbackCollector.SetReady(() => 
        {
            foreach (var completeAction in _transitionsDictionary.Values)
            {
                completeAction.SafeInvoke(true);
            }
            onComplete();
        });
    }

    public void ForceStop()
    {
        if (IsInProcess)
        {
            foreach (var transitionObject in _transitionsDictionary.Keys)
            {
                transitionObject.ForceStop();
            }
            foreach (var completeAction in _transitionsDictionary.Values)
            {
                completeAction.SafeInvoke(false);
            }
            _callbackCollector.ForceStop();
        }
    }

    public void Pause()
    {
        if (IsInProcess && !_isPaused)
        {
            _isPaused = true;
            foreach (IRoomTransition<R> transitionObject in _transitionsDictionary.Keys)
            {
                transitionObject.Pause();
            }
        }
    }

    public void UnPause()
    {
        if (IsInProcess && _isPaused)
        {
            _isPaused = false;
            foreach (IRoomTransition<R> transitionObject in _transitionsDictionary.Keys)
            {
                transitionObject.UnPause();
            }
        }
    }
}

