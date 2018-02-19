using UnityEngine;

public class HitCommand : Command
{
    [SerializeField] private Command[] _onAfterHitCommand;
    [SerializeField] private HitType _hitType;
    [SerializeField] private ClassInformation _classInformation;
    [SerializeField] private DamageSkill _damageSkill;

    private Fraction CurrentFraction
    {
        get
        {
            return _classInformation == null ? Fraction.Neutral :
                _classInformation.CurrentFraction;
        }
    }


    public override void Execute(GameObject actor)
    {
        HealthChanger actorHealth = actor.GetHealthChanger();

        if (actorHealth != null)
        {
            Fraction actorFraction = actor.GetFraction();

            if (actorFraction.IsHittableBy(CurrentFraction))
            {
                actorHealth.Hit(_damageSkill.DamageValue, _hitType);
                _onAfterHitCommand.Execute(actor);
            }
        }
    }
}
