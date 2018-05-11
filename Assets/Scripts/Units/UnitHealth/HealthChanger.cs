using System;
using UnityEngine;

public abstract class HealthChanger : MonoBehaviour
{
    [AutomaticSet] [SerializeField] private HealthContainer _healthContainer;

    protected HealthContainer HealthContainer { get { return _healthContainer; } }

    public HealthPoints Health { set { _healthContainer.CurrentHealth = value; } }
    public abstract bool Hit(HealthPoints value, HitType hitType);
    public abstract bool Heal(HealthPoints value);
    public abstract bool Kill(HitType hitType);

    public abstract event Action AfterHeal;
    public abstract event Action AfterHit;
}

public enum HitType
{
    Projectile,
    Explosion,
    Contact
}

