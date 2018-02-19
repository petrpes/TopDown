using UnityEngine;

public class DespawnCommand : Command
{
    [SerializeField] private GameObject _parent;
    [SerializeField] private bool _despawnSelf = true;

    public override void Execute(GameObject actor)
    {
        var despawnableObject = _despawnSelf ? (_parent == null ? gameObject : _parent) : actor;

        if (despawnableObject.activeSelf)
        {
            SpawnManager.Instance.Despawn(despawnableObject);
        }
    }
}

