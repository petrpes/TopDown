using Components.EventHandler;
using Components.Spawner.Pool;

public class SpawnManager : ISpawnManager
{
    public static ISpawnManager Instance = new SpawnManager();

    private PoolSpawner _roomsSpawner;
    private PoolSpawner _roomsTransferedSpawner;
    private PoolSpawner _projectileSpawner;
    private PoolSpawner _defaultSpawner;

    private SpawnManager()
    {
        _roomsSpawner = new PoolSpawner(1);
        _projectileSpawner = new PoolSpawner();
        _defaultSpawner = new PoolSpawner();
    }

    public void Despawn<T>(T spawnableObject)
    {
        var spawner = GetPoolSpawner(spawnableObject);
        spawner.Despawn(spawnableObject);
    }

    public T Spawn<T>(T prefab)
    {
        var spawner = GetPoolSpawner(prefab);
        return spawner.Spawn(prefab);
    }

    public void CreatePool<T>(T prefab, int poolCount)
    {
        var spawner = GetPoolSpawner(prefab);
        spawner.CreatePool(prefab, poolCount);
    }

    public void DestroyPool<T>(T prefab)
    {
        var spawner = GetPoolSpawner(prefab);
        spawner.Dispose();
    }

    private PoolSpawner GetPoolSpawner(object obj)
    {
        if (obj is Projectile)
        {
            return _projectileSpawner;
        }
        return _defaultSpawner;
    }
}

