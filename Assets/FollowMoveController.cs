using UnityEngine;

public class FollowMoveController : MoveController
{
    [SerializeField] private Transform _pusrued;
    private Transform _transform;

    public override bool GetControl(out DirectionVector direction)
    {
        if (_transform == null)
        {
            _transform = transform;
        }
        direction = new DirectionVector(_pusrued.position - _transform.position);
        return true;
    }
}

