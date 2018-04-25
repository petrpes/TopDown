using System;
using System.Collections;
using UnityEngine;

public class OnLevelOpenPutInACenter : MonoBehaviour, IRoomTransition
{
    public void TransitionToRoom(IRoom oldRoom, IRoom newRoom, Action onComplete)
    {
        transform.position = newRoom.Shape.Rectangle.center;
        onComplete.SafeInvoke();
    }

    private void Awake()
    {
        RoomTransitionProxy.Instance.SubscribeTransitionObject(this);
    }

    private void OnDestroy()
    {
        RoomTransitionProxy.Instance.UnsubscribeTransitionObject(this);
    }

    private void OnLevelStarted()
    {
        transform.position = LevelManager.Instance.CurrentRoom.Shape.Rectangle.center;
    }
}

