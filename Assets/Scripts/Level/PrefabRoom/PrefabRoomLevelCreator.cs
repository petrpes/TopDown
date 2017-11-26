using Components.Common;
using System;
using UnityEngine;

public class PrefabRoomLevelCreator : Singleton<PrefabRoomLevelCreator>, ILevelCreator, ILevelDestroyer
{
    [SerializeField] private PrefabRoomLibrary[] _levels;
    [SerializeField] private int[] _startingRoomIds;

    private int _currentLevelIndex = -1;

    public void CreateLevel(Action<Level> onLevelCreated)
    {
        _currentLevelIndex++;
        Level newLevel = new Level(_levels[_currentLevelIndex], _startingRoomIds[_currentLevelIndex]);
        for (int i = 0; i < newLevel.RoomLibrary.Count; i++)
        {
            SpawnManager.Instance.CreatePool(newLevel.RoomLibrary[i], 1);
        }
        onLevelCreated.SafeInvoke(newLevel);

        GameObject player = SpawnManager.Instance.Spawn(GameManager.Instance.PlayerPrefab);
        player.transform.position = newLevel.RoomLibrary[newLevel.StartingRoomId].Rectangle.center;
        GameManager.Instance.PlayerInstance = player;
    }

    public void DestroyLevel(Level level, Action onLevelDestroyed)
    {
        for (int i = 0; i < level.RoomLibrary.Count; i++)
        {
            SpawnManager.Instance.DestroyPool(level.RoomLibrary[i]);
        }
    }
}

