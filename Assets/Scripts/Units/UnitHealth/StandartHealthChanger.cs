using UnityEngine;

public class StandartHealthChanger : HealthChanger
{
    [SerializeField] private ClassInformation _classInformation;

    private Fraction CurrentFraction
    {
        get
        {
            return _classInformation == null ? Fraction.Neutral : 
                _classInformation.CurrentFraction;
        }
    }

    public override bool Heal(HealthPoints value)
    {
        if (AddToCurrentHealth(value))
        {
            InvokeOnAfterHealAction();
            return true;
        }

        return false;
    }

    public override bool Hit(HealthPoints value, HitType hitType)
    {
        if (!HealthContainer.IsInvincible && SubtractFromCurrentHealth(value, hitType))
        {
            if (HealthContainer.CurrentHealth <= HealthPoints.Zero)
            {
                HealthContainer.CurrentHealth = HealthPoints.Zero;
            }
            InvokeOnAfterHitAction();

            return true;
        }
        return false;
    }

    public override bool Kill(HitType hitType)
    {
        return Hit(HealthContainer.CurrentHealth, hitType);
    }

    protected virtual bool AddToCurrentHealth(HealthPoints health)
    {
        if (HealthContainer.CurrentHealth != HealthContainer.MaxHealth)
        {
            HealthContainer.CurrentHealth = 
                HealthPoints.Min(HealthContainer.CurrentHealth + health, HealthContainer.MaxHealth);
            return true;
        }
        return false;
    }

    protected virtual bool SubtractFromCurrentHealth(HealthPoints health, HitType hitType)
    {
        if (HealthContainer.CurrentHealth == HealthPoints.Zero)
        {
            return false;
        }
        HealthContainer.CurrentHealth =
            HealthPoints.Max(HealthContainer.CurrentHealth - health, HealthPoints.Zero);
        return true;
    }
}

