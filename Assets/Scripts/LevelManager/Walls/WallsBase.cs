using System;
using UnityEngine;

public abstract class WallsBase : MonoBehaviour, IDisposable
{
    /// <summary>
    /// Adding wall collider
    /// </summary>
    /// <param name="collider">Collider</param>
    /// <param name="wallLength">Length of the wall (distance between adjoining walls)</param>
    public abstract void AddWall(Orientation orientation, Collider2D collider);
    public abstract bool AddDoor(Orientation orientation, float position, float length);
    public abstract bool RemoveDoor(Orientation orientation, float position);
    public abstract void Dispose();
}

