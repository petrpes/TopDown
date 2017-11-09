using System;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private MoveController _controller;
    [SerializeField] private MobSkillSet _skillSet;

    public event Action<Vector3> MovingAction;

    private Rigidbody2D _rigidbody;

    void FixedUpdate ()
    {
        if (_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        DirectionVector direction;
        if (_controller.GetControl(out direction))
        {
            Vector3 speed = direction.Value * _skillSet.Speed;
            if (MovingAction != null)
            {
                MovingAction.Invoke(speed);
            }
            _rigidbody.velocity = speed;
        }
        else if (_rigidbody.velocity != Vector2.zero)
        {
            _rigidbody.velocity = Vector2.zero;
        }
    }
}
