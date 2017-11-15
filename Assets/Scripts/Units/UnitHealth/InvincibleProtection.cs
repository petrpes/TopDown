using Components.Timer;
using UnityEngine;

public class InvincibleProtection : MonoBehaviour
{
    [SerializeField] private float _invincibleTime;
    [SerializeField] private HealthContainer _healthContainer;
    [SerializeField] private HealthChanger _healthChanger;

    public float InvincibleTime { set { _invincibleTime = value; } }

    private ExpirationTimer _invincibilityTimer;
    private ExpirationTimer InvincibilityTimer
    {
        get
        {
            if (_invincibilityTimer == null)
            {
                _invincibilityTimer = new ExpirationTimer(_invincibleTime);
                _invincibilityTimer.OnExpiredTimer += TimerAction;
            }
            if (_invincibilityTimer.ExpirationTime != _invincibleTime)
            {
                _invincibilityTimer.ExpirationTime = _invincibleTime;
            }
            return _invincibilityTimer;
        }
    }

    private void Awake()
    {
        _healthChanger.OnAfterHitAction += OnAfterHitAction;
    }

    private void OnAfterHitAction()
    {
        _healthContainer.IsInvincible = true;
        InvincibilityTimer.Start();
    }

    private void TimerAction()
    {
        _healthContainer.IsInvincible = false;
    }
}

