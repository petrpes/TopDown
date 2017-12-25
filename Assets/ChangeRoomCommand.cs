using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoomCommand : Command
{
    [SerializeField] private TestRoom _room;

    public override void Execute(GameObject actor)
    {
        if (!LevelManager.Instance.CurrentRoom.Equals(_room))
        {
            LevelManager.Instance.CurrentRoom = _room;
        }
    }
}

