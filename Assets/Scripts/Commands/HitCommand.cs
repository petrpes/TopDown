using UnityEngine;

[RequireComponent(typeof(DamageSkill))]
public class HitCommand : Command
{
    [SerializeField] private Command[] _onHitCommand;

    private DamageSkill _damageSkill;

    public override void ExecuteCommand(GameObject actor)
    {
        if (_damageSkill == null)
        {
            _damageSkill = GetComponent<DamageSkill>();
        }

        UnitHealth actorHealth = actor.GetComponent<UnitHealth>();

        if (actorHealth != null)
        {
            DamageSkill actorDamage = actor.GetComponent<DamageSkill>();
            Fraction actorFraction = actorDamage == null ? Fraction.Neutral : actorDamage.CurrentFraction;

            if (actorFraction.IsHittableBy(_damageSkill.CurrentFraction))
            {
                actorHealth.Hit(_damageSkill.DamageValue, gameObject);
                if (_onHitCommand != null)
                {
                    for (int i = 0; i < _onHitCommand.Length; i++)
                    {
                        _onHitCommand[i].Execute(actor);
                    }
                }
            }
        }
    }
}
