using UnityEngine;

public class FollowMoveController : MoveController
{
    [SerializeField] private Transform _pusrued;
    private Transform _transform;

    public override bool GetControl(out Vector3 direction)
    {
        if (_transform == null)
        {
            _transform = transform;
        }
        direction = (_pusrued.position - _transform.position).normalized;
        return true;
    }
}

