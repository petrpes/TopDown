﻿using UnityEngine;

[RequireComponent(typeof(DamageSkill))]
public class HitCommand : Command
{
    [SerializeField] private Command[] _onAfterHitCommand;
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
            Fraction actorFraction = actor.GetFraction();

            if (actorFraction.IsHittableBy(CurrentFraction))
            {
                actorHealth.Hit(_damageSkill.DamageValue, _hitType);
                if (_onAfterHitCommand != null)
                {
                    _onAfterHitCommand.Execute(actor);
                }
            }
        }
    }
}
