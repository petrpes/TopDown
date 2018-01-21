using System;
using UnityEngine;

public abstract class Mover : MonoBehaviour
{
    public abstract event Action<Vector3> WalkingAction;

    public abstract Vector3 WalkingSpeed { get; }
    public abstract Vector3 MovingSpeed { get; }
    public abstract void AddForce(Vector3 force);
    public abstract Vector3 Position { get; set; }
    public abstract void ForceStop();
}

