﻿using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private class PositionDirectionInputArguments : IPositionInputArguments, IDirectionInputArguments
    {
        public bool IsPositionSet { get; private set; }

        private Vector3 _position;
        private DirectionVector _direction;

        public Vector3 Position
        {
            get { return _position; }
            set { _position = value; IsPositionSet = true; }
        }

        public DirectionVector Direction
        {
            get { return _direction; }
            set { _direction = value; IsPositionSet = false; }
        }
    }

    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _target;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private MobSkillSet _mobSkillSet;
    private Transform _transform;
    private PositionDirectionInputArguments _inputArguments;
    private Vector3 _lastPosition;

    void Start ()
    {
        _transform = transform;
        _inputArguments = new PositionDirectionInputArguments();
    }

    void FixedUpdate ()
    {
        if (InputManager.Instance.GetIsControl(InputAttributesSet.WeaponDirection, _inputArguments) ||
            _lastPosition != _transform.position)
        {
            if (_inputArguments.IsPositionSet)
            {
                _inputArguments.Direction = new DirectionVector(_inputArguments.Position - PlayerOnScreenPosition);
            }
            _lastPosition = _transform.position;
            _target.position = _lastPosition + _inputArguments.Direction.Value;
            float angle = Mathf.Atan2(_inputArguments.Direction.Value.y, _inputArguments.Direction.Value.x) * 
                Mathf.Rad2Deg - 90;
            _target.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        if (InputManager.Instance.GetIsControl(InputAttributesSet.WeaponShoot))
        {
            _weapon.Attack(_inputArguments.Direction);
        }
	}

    private Vector3 PlayerOnScreenPosition
    {
        get
        {
            return _camera.WorldToScreenPoint(_transform.position);
        }
    }
}
