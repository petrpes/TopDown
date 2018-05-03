using System;
using System.Collections;
using UnityEngine;

public class OnLevelOpenPutInACenter : MonoBehaviour
{
    [SerializeField] private Mover _mover;

    public void TransitionToRoom(IRoom oldRoom, IRoom newRoom, Action onComplete)
    {
        transform.position = newRoom.Shape.Rectangle.center;
        onComplete.SafeInvoke();
    }

    private void Awake()
    {
        LevelManager.Instance.OnAfterLevelCreated += OnLevelStarted;
    }

    private void OnDestroy()
    {
        LevelManager.Instance.OnAfterLevelCreated -= OnLevelStarted;
    }

    private void OnLevelStarted(ILevel level)
    {
        _mover.Position = LevelAPIs.CurrentRoom.Shape.Rectangle.center;
    }
}

