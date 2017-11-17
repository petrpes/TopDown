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
                OnMaxHealthChanged.SafeInvoke();
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
                OnCurrentHealthChanged.SafeInvoke();

                if (_currentHealth == HealthPoints.Zero)
                {
                    OnDeath.SafeInvoke();
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
                OnIsInvincibleChanged.SafeInvoke();
            }
        }
    }

    public event Action OnCurrentHealthChanged;
    public event Action OnMaxHealthChanged;
    public event Action OnIsInvincibleChanged;
    public event Action OnDeath;
}

