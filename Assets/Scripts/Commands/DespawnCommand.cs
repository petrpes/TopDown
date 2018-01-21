using UnityEngine;

public class DespawnCommand : Command
{
    [SerializeField] private Projectile _component;

    public override void Execute(GameObject actor)
    {
        SpawnManager.Instance.Despawn(_component);
    }
}

