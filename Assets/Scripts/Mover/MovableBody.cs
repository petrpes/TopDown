using UnityEngine;

public class MovableBody : MonoBehaviour
{
    [SerializeField] private Mover _mover;

    public void Push(Vector3 speed)
    {
        _mover.AddForce(speed);
    }

    public void ForceSetPosition(Vector3 position)
    {
        _mover.Position = position;
    }

    public void ForceStop()
    {
        _mover.ForceStop();
    }
}

