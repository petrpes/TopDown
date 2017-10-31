using UnityEngine;

public class HitCommand : Command
{
    public HealthPoints Damage { get; set; }

    public override void Execute(GameObject actor)
    {
        UnitHealth health = actor.GetComponent<UnitHealth>();
        if (health != null)
        {
            health.Hit(Damage);
        }
    }
}
