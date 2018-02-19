using Components.Spawner;
using Components.Spawner.Pool;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : ISpawnManager
{
    public static ISpawnManager Instance = new SpawnManager();

    private PoolSpawner _projectileSpawner;
    private PoolSpawner _defaultSpawner;

    private Dictionary<GameObject, ISpawnableObject> _spawnableObjectsCache;

    private SpawnManager()
    {
        _defaultSpawner = new PoolSpawner();
        _spawnableObjectsCache = new Dictionary<GameObject, ISpawnableObject>(10);
    }
    /*
    public T Spawn<T>(T prefab)
    {
        var spawner = GetPoolSpawner(prefab);
        var spawnedObject = spawner.Spawn(prefab);
        return spawnedObject;
    }*/

    public GameObject Spawn(GameObject prefab)
    {
        var spawner = GetPoolSpawner(prefab);
        var spawnedObject = spawner.Spawn(prefab);

        GetSpawnableObject(spawnedObject).SafeOnSpawned();

        return spawnedObject;
    }

    public bool Despawn(GameObject despawnableObject)
    {
        var spawner = GetPoolSpawner(despawnableObject);

        GetSpawnableObject(despawnableObject).SafeBeforeDespawned();

        return spawner.Despawn(despawnableObject);
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

    private ISpawnableObject GetSpawnableObject(GameObject gameObject)
    {
        if (_spawnableObjectsCache.ContainsKey(gameObject))
        {
            return _spawnableObjectsCache[gameObject];
        }
        var component = gameObject.GetSpawnableObject();
        _spawnableObjectsCache.Add(gameObject, component);
        return component;
    }
}

