using System;
using UnityEngine;

public class VelocityMover : Mover
{
    [AutomaticSet] [SerializeField] private Rigidbody2D _rigidbody;
    [AutomaticSet] [SerializeField] private WalkableSkillsSet _skillSet;
    [AutomaticSet] [SerializeField] private MoveController _controller;

    private Vector3 _walkingSpeed;
    private bool _isWalking;

    private bool _hasStopped;

    public override event Action<Vector3> WalkingAction;

    public override Vector3 WalkingSpeed { get { return _walkingSpeed; } }
    public override Vector3 MovingSpeed { get { return _rigidbody.velocity; } }

    private float Acceleration
    {
        get
        {
            float accelerationTime = _skillSet.AccelerationTime / Time.deltaTime;
            accelerationTime = accelerationTime < 1 ? 1 : accelerationTime;
            return _skillSet.Speed / accelerationTime;
        }
    }

    public override Vector3 Position
    {
        get
        {
            return _rigidbody.position;
        }
        set
        {
            _rigidbody.MovePosition(value);
        }
    }

    public override void AddForce(Vector3 speed)
    {
        _hasStopped = false;
        //_rigidbody.velocity += (Vector2)speed;
        _rigidbody.AddForce(speed, ForceMode2D.Impulse);
    }

    void FixedUpdate()
    {
        DirectionVector direction;

        if (_controller.GetControl(out direction))
        {
            _isWalking = true;
            _hasStopped = false;
            _walkingSpeed = direction * _skillSet.Speed;
        }
        else
        {
            _walkingSpeed = Vector3.zero;
        }

        if (_rigidbody.velocity == Vector2.zero && _walkingSpeed == Vector3.zero)
        {
            return;
        }

        Vector3 currentSpeedVector = Vector3.Lerp(_rigidbody.velocity, _walkingSpeed, Acceleration);

        if (!_hasStopped)
        {
            _rigidbody.velocity = currentSpeedVector;

            _hasStopped = currentSpeedVector.Equals(Vector3.zero);

            if (_isWalking)
            {
                _isWalking = !_hasStopped;
            }
        }

        if (_isWalking)
        {
            WalkingAction.SafeInvoke(currentSpeedVector);
        }
    }

    public override void ForceStop()
    {
        _rigidbody.velocity = Vector3.zero;
        _hasStopped = true;
        _isWalking = false;
    }
}

