using UnityEngine;

public class StandardUnitHealth : UnitHealth
{
    [SerializeField] private HealthPoints _maxHealth;
    [SerializeField] private HealthPoints _currentHealth;

    public override HealthPoints MaxHealth { get { return _maxHealth; } set { _maxHealth = value; } }
    public override HealthPoints CurrentHealth { get { return _currentHealth; } protected set { _currentHealth = value; } }

    public override bool AddToCurrentHealth(HealthPoints health, GameObject source)
    {
        CurrentHealth += health;
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
        return true;
    }

    public override bool SubtractFromCurrentHealth(HealthPoints health, GameObject source)
    {
        CurrentHealth -= health;
        return true;
    }
}

