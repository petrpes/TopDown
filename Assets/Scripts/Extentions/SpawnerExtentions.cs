using Components.Spawner;

public static class SpawnerExtentions
{
    public static void SafeOnSpawned(this ISpawnableObject spawnableObject)
    {
        if (spawnableObject != null)
        {
            spawnableObject.OnAfterSpawn();
        }
    }

    public static void SafeBeforeDespawned(this ISpawnableObject spawnableObject)
    {
        if (spawnableObject != null)
        {
            spawnableObject.OnBeforeDespawn();
        }
    }
}

