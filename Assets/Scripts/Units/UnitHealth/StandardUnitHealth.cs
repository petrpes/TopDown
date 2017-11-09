using UnityEngine;

public class StandardUnitHealth : UnitHealth
{
    [SerializeField] private HealthPoints _maxHealth;
    [SerializeField] private HealthPoints _currentHealth;

    public override HealthPoints CurrentHealth
    {
        get
        {
            return _currentHealth;
        }
    }

    public override bool Heal(HealthPoints health)
    {
        if (_currentHealth + health > _maxHealth)
        {
            health = _maxHealth - _currentHealth;
        }
        _currentHealth += health;

        if (health != HealthPoints.Zero)
        {
            InvokeHealAction();
            return true;
        }
        return false;
    }

    public override bool Hit(HealthPoints health)
    {
        if (!IsInvincible)
        {
            _currentHealth -= health;
            if (_currentHealth > HealthPoints.Zero)
            {
                InvokeHitAction();
            }
            else
            {
                _currentHealth = HealthPoints.Zero;
                IsInvincible = true;

                InvokeDeathAction();
            }
            return true;
        }
        return false;
    }
}

