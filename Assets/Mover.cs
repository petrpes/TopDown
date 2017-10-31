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

        Vector3 direction;
        if (_controller.GetControl(out direction))
        {
            Vector3 speed = direction * _skillSet.Speed;
            MovingAction.Invoke(speed);
            _rigidbody.velocity = speed;
        }
        else if (_rigidbody.velocity != Vector2.zero)
        {
            _rigidbody.velocity = Vector3.zero;
        }
    }
}
