using UnityEngine;

public class PutInARoomCommand : Command
{
    [SerializeField] private Vector2 _positionInARoom;
    [AutomaticSet] [SerializeField] private Mover _mover;

    public override void Execute(GameObject actor)
    {
        if (_mover != null)
        {
            Rect roomRectangle = LevelAPIs.CurrentRoom.Shape.Rectangle;

            _mover.Position = roomRectangle.min +
                (roomRectangle.max - roomRectangle.min).Multiply(_positionInARoom);
        }
    }
}

