using UnityEngine;

public class FollowMoveController : MoveController
{
    [SerializeField] private Mover _pursued;
    [SerializeField] private Mover _mover;

    public override bool GetControl(out DirectionVector direction)
    {
        if (_pursued == null)
        {
            _pursued = GameManager.Instance.Player.GetLevelObjectComponent<Mover>();
        }
        direction = new DirectionVector(_pursued.Position - _mover.Position);
        return true;
    }
}

