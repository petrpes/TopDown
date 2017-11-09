using System;
using UnityEngine;

public abstract class UnitHealth : MonoBehaviour
{
    public abstract HealthPoints CurrentHealth { get; }

    public abstract bool Heal(HealthPoints health);

    public abstract bool Hit(HealthPoints health);

    public virtual bool IsInvincible { get; set; }

    public event Action AfterHealAction;
    public event Action AfterHitAction;
    public event Action DeathAction;

    protected void InvokeHealAction()
    {
        if (AfterHealAction != null)
        {
            AfterHealAction.Invoke();
        }
    }

    protected void InvokeHitAction()
    {
        if (AfterHitAction != null)
        {
            AfterHitAction.Invoke();
        }
    }

    protected void InvokeDeathAction()
    {
        if (DeathAction != null)
        {
            DeathAction.Invoke();
        }
    }
}

