using UnityEngine;

[RequireComponent(typeof(DamageSkill))]
public class HitCommand : Command
{
    [SerializeField] private Command[] _onHitCommand;
    [SerializeField] private HitType _hitType;
    [SerializeField] private ClassInformation _classInformation;

    private Fraction CurrentFraction
    {
        get
        {
            return _classInformation == null ? Fraction.Neutral :
                _classInformation.CurrentFraction;
        }
    }

    private DamageSkill _damageSkill;

    public override void Execute(GameObject actor)
    {
        if (_damageSkill == null)
        {
            _damageSkill = GetComponent<DamageSkill>();
        }

        HealthChanger actorHealth = actor.GetComponent<HealthChanger>();

        if (actorHealth != null)
        {
            DamageSkill actorDamage = actor.GetComponent<DamageSkill>();
            Fraction actorFraction = actor.GetFraction();

            if (actorFraction.IsHittableBy(CurrentFraction))
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
