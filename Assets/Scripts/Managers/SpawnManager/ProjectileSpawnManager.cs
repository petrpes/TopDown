using Components.EventHandler;
using Components.Spawner.Pool;

public class ProjectileSpawnManager : ISpawnManager, IEventListener<RoomChangedEventArguments>,
    IEventListener<LevelChangedEventArguments>
{
    private PoolSpawner _pool;

    public ProjectileSpawnManager()
    {
        _pool = new PoolSpawner();
    }

    public void CreatePool<T>(T prefab, int instancesCount)
    {
        _pool.CreatePool(prefab, instancesCount);
    }

    public void Despawn<T>(T spawnableObject)
    {
        _pool.Despawn(spawnableObject);
    }

    public void DestroyPool<T>(T prefab)
    {
        _pool.Dispose();
    }

    public void HandleEvent(RoomChangedEventArguments arguments, object sender)
    {
        throw new System.NotImplementedException();
    }

    public void HandleEvent(LevelChangedEventArguments arguments, object sender)
    {
        if (arguments.NewLevel == null)
        {
            _pool.Dispose();
        }
    }

    public T Spawn<T>(T prefab)
    {
        return _pool.Spawn(prefab);
    }
}

