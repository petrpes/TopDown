public class BulletSpawner
{
    public static BulletSpawner Instance = new BulletSpawner();

    private PoolSpawner _spawner = new PoolSpawner();

    public void Despawn(Projectile spawnableObject)
    {
        _spawner.Despawn(spawnableObject);
    }

    public Projectile Spawn(Projectile prefab)
    {
        return _spawner.Spawn(prefab);
    }
}

