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
    public abstract void AddForce(Vector3 force);
    public abstract Vector3 Position { get; set; }
    public abstract void ForceStop();
}

