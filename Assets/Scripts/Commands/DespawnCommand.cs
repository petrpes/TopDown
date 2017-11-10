using UnityEngine;

public class DespawnCommand : Command
{
    private Projectile _projectile;

    public override void ExecuteCommand(GameObject actor)
    {
        if (_projectile == null)
        {
            _projectile = GetComponent<Projectile>();
        }
        BulletSpawner.Instance.Despawn(_projectile);
    }
}

