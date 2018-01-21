using UnityEngine;

public class PlayerViewController : MonoBehaviour
{
    [SerializeField] private HealthContainer _healthContainer;
    [SerializeField] private HealthChanger _healthChanger;
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private Mover _mover;

    private void Awake()
    {
        if (_healthContainer != null)
        {
            _healthContainer.Death += Die;
        }

        if (_healthContainer != null)
        {
            _healthContainer.InvincibleChanged += SetInvincibvle;
        }

        if (_mover != null)
        {
            _mover.WalkingAction += MovingAction;
        }
    }

    private void MovingAction(Vector3 speed)
    {
        if (speed.x != 0)
        {
            _playerView.IsLookingLeft = speed.x < 0;
        }
    }

    private void Die()
    {
        _playerView.StartDeathAnimation();
    }

    private void SetInvincibvle()
    {
        if (_healthContainer.IsInvincible)
        {
            _playerView.StartHitAnimation();
        }
        else
        {
            _playerView.StopHitAnimation();
        }
    }
}

