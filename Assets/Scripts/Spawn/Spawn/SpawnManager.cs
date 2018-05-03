using Components.Spawner;
using Components.Spawner.Pool;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnManager : ISpawnManager
{
    private PoolSpawner _defaultSpawner;

    private Dictionary<GameObject, ISpawnableObject> _spawnableObjectsCache;

    public event Action<object> AfterObjectSpawned;
    public event Action<object> BeforeObjectDespawned;

    public SpawnManager()
    {
        _defaultSpawner = new PoolSpawner();
        _spawnableObjectsCache = new Dictionary<GameObject, ISpawnableObject>(10);
    }

    public GameObject Spawn(GameObject prefab)
    {
        var spawner = GetPoolSpawner(prefab);
        var spawnedObject = spawner.Spawn(prefab);

        AfterObjectSpawned.SafeInvoke(spawnedObject);

        return spawnedObject;
    }

    public bool Despawn(GameObject despawnableObject)
    {
        if (despawnableObject == null)
        {
            return false;
        }

        var spawner = GetPoolSpawner(despawnableObject);

        BeforeObjectDespawned.SafeInvoke(despawnableObject);

        if (despawnableObject.activeSelf)
        {
            if (!spawner.Despawn(despawnableObject))
            {
                GameObject.Destroy(despawnableObject);
            }
            return true;
        }

        return false;
    }

    public void CreatePool(GameObject prefab, int poolCount)
    {
        var spawner = GetPoolSpawner(prefab);
        spawner.CreatePool(prefab, poolCount);
    }

    public void Dispose()
    {
        _defaultSpawner.Dispose();
    }

    private PoolSpawner GetPoolSpawner(object obj)
    {
        return _defaultSpawner;
    }

    public bool IsSpawned(GameObject obj)
    {
        return obj.activeSelf;
    }
}

