using System;
using UnityEngine;

public abstract class UnitHealth : MonoBehaviour
{
    public virtual HealthPoints MaxHealth { get; set; }
    public virtual HealthPoints CurrentHealth { get; protected set; }

    public bool Heal(HealthPoints health, GameObject source)
    {
        if (CurrentHealth != MaxHealth && AddToCurrentHealth(health, source))
        {
            if (AfterHealAction != null)
            {
                AfterHealAction.Invoke();
            }
            return true;
        }

        return false;
    }

    public bool Hit(HealthPoints health, GameObject source)
    {
        if (!IsInvincible && SubtractFromCurrentHealth(health, source))
        {
            if (CurrentHealth <= HealthPoints.Zero)
            {
                CurrentHealth = HealthPoints.Zero;

                if (DeathAction != null)
                {
                    DeathAction.Invoke();
                }
            }
            else
            {
                if (AfterHitAction != null)
                {
                    AfterHitAction.Invoke();
                }
            }

            return true;
        }
        return false;
    }

    public abstract bool SubtractFromCurrentHealth(HealthPoints health, GameObject source);
    public abstract bool AddToCurrentHealth(HealthPoints health, GameObject source);

    public virtual bool IsInvincible { get; set; }

    public event Action AfterHealAction;
    public event Action AfterHitAction;
    public event Action DeathAction;
}

