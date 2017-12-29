using Components.Spawner;

public interface ISpawnManager : ISpawner, IDespawner
{
    void CreatePool<T>(T prefab, int instancesCount);
    void DestroyPool<T>(T prefab);
    bool IsSpawned<T>(T obj);
}

