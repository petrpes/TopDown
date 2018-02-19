using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    IRoom[] _rooms;

	void Start ()
    {
        _rooms = new IRoom[TestLevelMap.Instance.RoomsCount];
        int i = 0;
        foreach (IRoom room in TestLevelMap.Instance.GetRooms())
        {
            _rooms[i] = room;
            i++;
        }

        LevelManager.Instance.LoadNextLevel(null);
	}
}
