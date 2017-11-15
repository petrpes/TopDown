using UnityEngine;

[RequireComponent(typeof(DamageSkill))]
public class HitCommand : Command
{
    [SerializeField] private Command[] _onHitCommand;
    [SerializeField] private HitType _hitType;
    [SerializeField] private ClassInformation _classInformation;

    private DamageSkill _damageSkill;

    public override void ExecuteCommand(GameObject actor)
    {
        Fraction fraction = _classInformation == null ? Fraction.Neutral : _classInformation.CurrentFraction;

        if (_damageSkill == null)
        {
            _damageSkill = GetComponent<DamageSkill>();
        }

        HealthChanger actorHealth = actor.GetComponent<HealthChanger>();

        if (actorHealth != null)
        {
            DamageSkill actorDamage = actor.GetComponent<DamageSkill>();
            ClassInformation actorClassInformation = actor.GetComponent<ClassInformation>();
            Fraction actorFraction = actorClassInformation == null ? Fraction.Neutral : 
                actorClassInformation.CurrentFraction;

            if (actorFraction.IsHittableBy(fraction))
            {
                actorHealth.Hit(_damageSkill.DamageValue, _hitType);
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
