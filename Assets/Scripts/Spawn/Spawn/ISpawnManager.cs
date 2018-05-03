using Components.Spawner;
using System;
using UnityEngine;

public interface ISpawnManager : ISpawner<GameObject>, IDespawner<GameObject>, IDisposable
{
    void CreatePool(GameObject prefab, int instancesCount);
    bool IsSpawned(GameObject obj);

    event Action<object> AfterObjectSpawned;
    event Action<object> BeforeObjectDespawned;
}

