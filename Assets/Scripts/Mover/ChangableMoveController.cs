using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangableMoveController : MoveController
{
    [SerializeField] private MoveController _idleMoveController;
    [SerializeField] private MoveController _agroMoveController;

    private MoveController _currentController;

    [System.NonSerialized] private MoveStateEnum _moveState;
    [System.NonSerialized] private bool _isSet = false;

    public MoveStateEnum MoveState
    {
        set
        {
            if (value != _moveState || !_isSet)
            {
                _moveState = value;
                _isSet = true;

                _currentController.SafeSetEnabled(false);

                switch (_moveState)
                {
                    case MoveStateEnum.Idle: _currentController = _idleMoveController; break;
                    case MoveStateEnum.Agro: _currentController = _agroMoveController; break;
                }

                _currentController.SafeSetEnabled(true);
            }
        }
    }

    public override bool GetControl(out DirectionVector direction)
    {
        if (_currentController == null)
        {
            direction = DirectionVector.Zero;
            return false;
        }
        return _currentController.GetControl(out direction);
    }
}

public enum MoveStateEnum
{
    Idle,
    Agro
}

