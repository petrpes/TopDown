using UnityEngine;

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

    [SerializeField] private Transform _targetArrow;
    [SerializeField] private Weapon _weapon;

    private Camera _camera;

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
            _targetArrow.position = _lastPosition + _inputArguments.Direction.Value;
            _targetArrow.rotation = _inputArguments.Direction.Value.RotationTowards();
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
            if (_camera == null)
            {
                _camera = Camera.main;
            }
            return _camera.WorldToScreenPoint(_transform.position);
        }
    }
}
