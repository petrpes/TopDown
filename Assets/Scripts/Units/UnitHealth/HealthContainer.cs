using System;
using UnityEngine;

public class HealthContainer : MonoBehaviour
{
    [SerializeField] private HealthPoints _maxHealth;
    [SerializeField] private HealthPoints _currentHealth;
    [SerializeField] private bool _isInvincible = false;

    public HealthPoints MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            if (_maxHealth != value)
            {
                _maxHealth = value;
                MaxHealthChanged.SafeInvoke();
            }
        }
    }

    public HealthPoints CurrentHealth
    {
        get
        {
            return _currentHealth;
        }
        set
        {
            if (_currentHealth != value)
            {
                _currentHealth = value;
                CurrentHealthChanged.SafeInvoke();

                if (_currentHealth == HealthPoints.Zero)
                {
                    Death.SafeInvoke();
                }
            }
        }
    }

    public bool IsInvincible
    {
        get
        {
            return _isInvincible;
        }
        set
        {
            if (_isInvincible != value)
            {
                _isInvincible = value;
                InvincibleChanged.SafeInvoke();
            }
        }
    }

    public event Action CurrentHealthChanged;
    public event Action MaxHealthChanged;
    public event Action InvincibleChanged;
    public event Action Death;
}

