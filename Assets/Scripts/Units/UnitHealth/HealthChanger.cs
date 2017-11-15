using System;
using UnityEngine;

public abstract class HealthChanger : MonoBehaviour
{
    [SerializeField] private HealthContainer _healthContainer;

    protected HealthContainer HealthContainer { get { return _healthContainer; } }

    public HealthPoints Health { set { _healthContainer.CurrentHealth = value; } }
    public abstract bool Hit(HealthPoints value, HitType hitType);
    public abstract bool Heal(HealthPoints value);
    public abstract bool Kill(HitType hitType);

    public event Action OnAfterHealAction;
    public event Action OnAfterHitAction;
    public event Action OnAfterDeathAction;

    protected void InvokeOnAfterHealAction() { OnAfterHealAction.SafeInvoke(); }
    protected void InvokeOnAfterHitAction() { OnAfterHitAction.SafeInvoke(); }
    protected void InvokeOnAfterDeathAction() { OnAfterDeathAction.SafeInvoke(); }
}

public enum HitType
{
    Projectile,
    Explosion
}

