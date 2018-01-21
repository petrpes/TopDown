using Components.Spawner;
using UnityEngine;

public class SpawnableObjectsProxy : MonoBehaviour, ISpawnableObject
{
    [SerializeField] private ComponentsCache _spawnableObjects = new ComponentsCache(typeof(ISpawnableObject), true, false);

    public void OnAfterSpawn()
    {
        foreach (var spawnableObject in _spawnableObjects.GetCachedComponets<ISpawnableObject>())
        {
            if (!spawnableObject.Equals(this))
            {
                spawnableObject.OnAfterSpawn();
            }
        }
    }

    public void OnBeforeDespawn()
    {
        foreach (var spawnableObject in _spawnableObjects.GetCachedComponets<ISpawnableObject>())
        {
            if (!spawnableObject.Equals(this))
            {
                spawnableObject.OnBeforeDespawn();
            }
        }
    }
}

