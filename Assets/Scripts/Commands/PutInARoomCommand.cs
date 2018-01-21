using UnityEngine;

public class PutInARoomCommand : Command
{
    [SerializeField] private Vector2 _positionInARoom;

    public override void Execute(GameObject actor)
    {
        Mover mover = actor.GetMover();
        if (mover != null)
        {
            Rect roomRectangle = LevelManager.Instance.CurrentRoom.Rectangle;

            mover.Position = roomRectangle.min +
                (roomRectangle.max - roomRectangle.min).Multiply(_positionInARoom);
        }
    }
}

