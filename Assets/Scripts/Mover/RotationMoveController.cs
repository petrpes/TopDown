using UnityEngine;

public class RotationMoveController : MoveController
{
    [SerializeField] private Vector2 _position;
    [SerializeField] private float _radius;
    [SerializeField] private Mover _mover;

    public override bool GetControl(out DirectionVector direction)
    {
        Vector3 globalPositon = 
            LevelManager.Instance.CurrentRoom.Shape.Rectangle.center + _position;

        var distance = globalPositon - _mover.Position;
        var distRotate = Quaternion.Euler(0, 0, 90) * distance;
        if (distance.magnitude <= _radius)
        {
            distance = Vector3.zero;
        }
        var res = distance + distRotate;
        direction = new DirectionVector(res);
        return true;
    }
}

