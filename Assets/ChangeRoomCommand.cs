using UnityEngine;

public class ChangeRoomCommand : Command
{
    [SerializeField] private Door _door;

    public override void Execute(GameObject actor)
    {
        if (!LevelAPIs.CurrentRoom.Equals(_door.RoomTo))
        {
            LevelAPIs.ChangeRoom(_door.RoomTo);
        }
    }
}

