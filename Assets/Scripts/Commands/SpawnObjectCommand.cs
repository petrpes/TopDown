using UnityEngine;

public class SpawnObjectCommand : Command
{
    [SerializeField] private SpawnPointBase _spawnPoint;

    private Transform _transform;

    public override void Execute(GameObject actor)
    {
        _spawnPoint.Spawn();
    }
}

