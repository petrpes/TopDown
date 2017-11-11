using System;
using UnityEngine;

[RequireComponent(typeof(WalkableSkillsSet))]
[RequireComponent(typeof(MoveController))]
public class Mover : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    public event Action<Vector3> MovingAction;

    private WalkableSkillsSet _skillSet;
    private MoveController _controller;

    public Vector3 CurrentSpeedVector { get; private set; }
    private bool _hasStopped;

    private float Acceleration
    {
        get
        {
            float accelerationTime = _skillSet.AccelerationTime / Time.deltaTime;
            accelerationTime = accelerationTime < 1 ? 1 : accelerationTime;
            return _skillSet.Speed / accelerationTime;
        }
    }

    void FixedUpdate ()
    {
        if (_skillSet == null)
        {
            _skillSet = GetComponent<WalkableSkillsSet>();
        }
        if (_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
        if (_controller == null)
        {
            _controller = GetComponent<MoveController>();
        }

        Vector3 desiredSpeedVector;

        DirectionVector direction;
        if (_controller.GetControl(out direction))
        {
            _hasStopped = false;
            desiredSpeedVector = direction.Value * _skillSet.Speed;
        }
        else
        {
            desiredSpeedVector = Vector3.zero;
        }

        CurrentSpeedVector = Vector3.Lerp(CurrentSpeedVector, desiredSpeedVector, Acceleration);

        if (!_hasStopped)
        {
            if (MovingAction != null)
            {
                MovingAction.Invoke(desiredSpeedVector);
            }

            _rigidbody.velocity = CurrentSpeedVector;

            _hasStopped = CurrentSpeedVector.Equals(Vector3.zero);
        }
    }
}
