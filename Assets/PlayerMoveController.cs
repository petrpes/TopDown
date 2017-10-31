using UnityEngine;

public class PlayerMoveController : MoveController
{
    private class MoveDirection : IDirectionInputArguments
    {
        public Vector3 Direction { get; set; }
    }

    private MoveDirection _moveDirection;

    public override bool GetControl(out Vector3 direction)
    {
        bool result = false;
        if (_moveDirection == null)
        {
            _moveDirection = new MoveDirection();
        }
        result = InputManager.Instance.GetIsControl(InputAttributesSet.Walk, _moveDirection);
        direction = _moveDirection.Direction;
        return result;
    }
}

