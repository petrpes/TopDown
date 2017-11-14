using System;
using UnityEngine;

public abstract class Mover : MonoBehaviour
{
    public event Action<Vector3> WalkingAction;

    protected void InvokeWalkingAction(Vector3 speed)
    {
        if (WalkingAction != null)
        {
            WalkingAction.Invoke(speed);
        }
    }

    public abstract Vector3 WalkingSpeed { get; }

    public abstract void Push(Vector3 speed);

    public abstract void ForceStop();

    public abstract void ForceSetPosition(Vector3 position);
}

