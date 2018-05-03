using System;
using Components.RoomsManager;
using UnityEngine;

public class OnTransitionSetRoomActive : IRoomsHooksMediator<RoomEventType, IRoom, object>, IRoomTransition<IRoom>
{
    public bool IsInProcess { get { return false; } }

    private IRoom _newRoom;
    private IRoom _oldRoom;

    public void Connect(RoomsManager<RoomEventType, IRoom, object> manager)
    {
        manager.TransitionHandler.SubscribeTransistor(this, OnCompleteAction);
    }

    public void InvokeTransitionAction(IRoom oldRoom, IRoom newRoom, Action onComplete)
    {
        _newRoom = newRoom;
        _oldRoom = oldRoom;

        if (oldRoom != null)
        {
            (oldRoom as Component).gameObject.SetActive(false);
        }
        if (newRoom != null)
        {
            (newRoom as Component).gameObject.SetActive(true);
        }
        onComplete.SafeInvoke();
    }

    public void OnCompleteAction(bool isSuccess)
    {
        if (!isSuccess)
        {
            if (_newRoom != null)
            {
                (_newRoom as Component).gameObject.SetActive(false);
            }
            if (_oldRoom != null)
            {
                (_oldRoom as Component).gameObject.SetActive(true);
            }
        }
    }

    public void ForceStop() { }

    public void Pause() { }

    public void UnPause() { }
}

