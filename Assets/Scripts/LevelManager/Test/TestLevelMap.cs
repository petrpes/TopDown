using System;
using System.Collections.Generic;
using UnityEngine;

public class TestLevelMap : MonoBehaviour, ILevelMap
{
    public static TestLevelMap Instance;

    [SerializeField] private TestRoom[] _rooms;
    [SerializeField] private Transform _player;//todo not in here

    public Transform Player
    {
        get
        {
            return _player;
        }
    }

    public int RoomsCount
    {
        get
        {
            return _rooms.Length;
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerable<IRoom> GetRooms(Func<IRoom, bool> predicate = null)
    {
        for (int i = 0; i < _rooms.Length; i++)
        {
            if (predicate == null || predicate(_rooms[i]))
            {
                yield return _rooms[i];
            }
        }
    }
}

