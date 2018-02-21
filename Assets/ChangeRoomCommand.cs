using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoomCommand : Command
{
    [SerializeField] private Door _door;

    public override void Execute(GameObject actor)
    {
        if (!LevelManager.Instance.CurrentRoom.Equals(_door.RoomTo))
        {
            LevelManager.Instance.CurrentRoom = _door.RoomTo;
        }
    }
}

