using UnityEngine;

public class BlastDamageUnitHealth : UnitHealth
{
    public override bool AddToCurrentHealth(HealthPoints health, GameObject source)
    {
        throw new System.NotImplementedException();
    }

    public override bool SubtractFromCurrentHealth(HealthPoints health, GameObject source)
    {
        if (source.layer == 15)
        {
            CurrentHealth -= new HealthPoints(1);
            return false;
        }
        return false;
    }
}

