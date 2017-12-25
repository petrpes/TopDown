using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLevelOpenPutInACenter : RoomEventListener
{
    protected override IRoom Room
    {
        get
        {
            return LevelManager.Instance.CurrentLevel.StartRoom;
        }
    }

    private void OnAfterLevelStarted()
    {
        transform.position = Room.Rectangle.center;
    }
}

