using UnityEngine;

public class DespawnCommand : Command
{
    [SerializeField] private GameObject _objectToDespawn;

    public override void Execute(GameObject actor)
    {
        ObjectsAPI.DespawnObject(_objectToDespawn == null ? gameObject : _objectToDespawn);
    }
}

