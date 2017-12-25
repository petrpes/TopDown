using System;
using System.Collections.Generic;
using UnityEngine;

public class TestLevelMap : MonoBehaviour, ILevelMap
{
    public static TestLevelMap Instance;

    [SerializeField] private TestRoom[] _roomsPrefabs;

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerable<IRoom> GetRooms<T>(Func<T, bool> predicate) where T : IRoomsPredicateArguments
    {
        foreach (IRoom room in GetRooms())
        {
            yield return room;
        }
    }

    public IEnumerable<IRoom> GetRooms()
    {
        for (int i = 0; i < _roomsPrefabs.Length; i++)
        {
            yield return _roomsPrefabs[i];
        }
    }
}

