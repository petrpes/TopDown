using UnityEngine;

public class BlastDamageHealthChanger : StandartHealthChanger
{
    protected override bool SubtractFromCurrentHealth(HealthPoints health, HitType hitType)
    {
        if (HealthContainer.CurrentHealth == HealthPoints.Zero || hitType != HitType.Explosion)
        {
            return false;
        }
        HealthContainer.CurrentHealth =
            HealthPoints.Max(HealthContainer.CurrentHealth - health, HealthPoints.Zero);
        return true;
    }
}

