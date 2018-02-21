using System;
using System.Collections;
using UnityEngine;

public class OnLevelOpenPutInACenter : MonoBehaviour, ICoroutineCollectionWriter<RoomTransitionArguments>
{
    public IEnumerator Coroutine(Action onComplete, RoomTransitionArguments args)
    {
        transform.position = args.NewRoom.Shape.Rectangle.center;
        yield return null;
        onComplete.SafeInvoke();
    }

    private void Awake()
    {
        //LevelManager.Instance.OnAfterLevelStarted += OnLevelStarted;
        RoomTransitionInvoker.Instance.SubscribeCoroutine(this);
    }

    private void OnDestroy()
    {
        //LevelManager.Instance.OnAfterLevelStarted -= OnLevelStarted;
        RoomTransitionInvoker.Instance.UnsubscribeCoroutine(this);
    }

    private void OnLevelStarted()
    {
        transform.position = LevelManager.Instance.CurrentRoom.Shape.Rectangle.center;
    }
}

