using System;
using System.Collections.Generic;

public class RoomTransitionProxy : IRoomTransition
{
    public static RoomTransitionProxy Instance = new RoomTransitionProxy();

    private CallbackCollector _callbackCollector;
    private List<IRoomTransition> _objectsList;

    public RoomTransitionProxy()
    {
        _callbackCollector = new CallbackCollector();
        _objectsList = new List<IRoomTransition>();
    }

    public void SubscribeTransitionObject(IRoomTransition transitionObject)
    {
        _objectsList.Add(transitionObject);
    }

    public void UnsubscribeTransitionObject(IRoomTransition transitionObject)
    {
        _objectsList.Remove(transitionObject);
    }

    public void TransitionToRoom(IRoom oldRoom, IRoom newRoom, Action onComplete)
    {
        foreach (IRoomTransition transitionObject in _objectsList)
        {
            transitionObject.TransitionToRoom(oldRoom, newRoom, _callbackCollector.AddCallback());
        }
        _callbackCollector.SetReady(onComplete);
    }
}

