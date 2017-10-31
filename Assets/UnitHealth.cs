using System;
using UnityEngine;

public class UnitHealth : MonoBehaviour
{
    [SerializeField] private HealthPoints _maxHealth;
    [SerializeField] private HealthPoints _currentHealth;

    public HealthPoints CurrentHealth
    {
        get
        {
            return _currentHealth;
        }
    }

    public event Action AfterHealAction;
    public event Action AfterHitAction;
    public event Action DeathAction;

    public bool Heal(HealthPoints health)
    {
        if (_currentHealth + health > _maxHealth)
        {
            health = _maxHealth - _currentHealth;
        }
        _currentHealth += health;

        if (health != HealthPoints.Zero)
        {
            AfterHealAction.Invoke();
            return true;
        }
        return false;
    }

    public bool Hit(HealthPoints health)
    {
        if (!IsInvincible)
        {
            _currentHealth -= health;
            if (_currentHealth > HealthPoints.Zero)
            {
                if (AfterHitAction != null)
                {
                    AfterHitAction.Invoke();
                }
            }
            else
            {
                if (DeathAction != null)
                {
                    DeathAction.Invoke();
                }
            }
            return true;
        }
        return false;
    }

    public bool IsInvincible { get; set; }
}
