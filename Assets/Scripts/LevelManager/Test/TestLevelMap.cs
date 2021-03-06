﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class TestLevelMap : MonoBehaviour, ILevelMap
{
    public static TestLevelMap Instance;

    [SerializeField] private TestRoom[] _rooms;

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

    public IEnumerable<IRoom> GetRooms(Predicate<IRoom> predicate = null)
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

