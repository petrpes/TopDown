using UnityEngine;
using Components.Spawner.Pool;

public class SpawnObjectCommand : Command
{
    [SerializeField] private GameObject _object;

    private PoolSpawner _spawner;

    public override void ExecuteCommand(GameObject actor)
    {
        if (_spawner == null)
        {
            _spawner = new PoolSpawner(1);
        }
        GameObject gameObject = _spawner.Spawn(_object);
        gameObject.transform.position = transform.position;
    }
}

