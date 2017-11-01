using UnityEngine;

[RequireComponent(typeof(DamageSkill))]
public class HitCommand : Command
{
    private DamageSkill _damageSkill;

    public override void ExecuteCommand(GameObject actor)
    {
        if (_damageSkill == null)
        {
            _damageSkill = GetComponent<DamageSkill>();
        }

        UnitHealth health = actor.GetComponent<UnitHealth>();
        if (health != null)
        {
            health.Hit(_damageSkill.DamageValue);
        }
    }
}
