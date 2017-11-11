using System.Collections;
using UnityEngine;

public class PlayerViewController : MonoBehaviour
{
    [SerializeField] private UnitHealth _unitHealth;
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private float _invincibleTime;
    [SerializeField] private Mover _mover;

    private void Awake()
    {
        if (_unitHealth != null)
        {
            _unitHealth.AfterHitAction += AfterHitAction;
            _unitHealth.DeathAction += Die;
        }

        if (_mover != null)
        {
            _mover.MovingAction += MovingAction;
        }
    }

    private void AfterHitAction()
    {
        StartCoroutine(SetInvincible());
    }

    private void MovingAction(Vector3 speed)
    {
        if (speed.x != 0)
        {
            _playerView.IsLookingLeft = speed.x < 0;
        }
    }

    private IEnumerator SetInvincible()
    {
        _unitHealth.IsInvincible = true;
        _playerView.StartHitAnimation();

        yield return new WaitForSeconds(_invincibleTime);

        _unitHealth.IsInvincible = false;
        _playerView.StopHitAnimation();
    }

    private void Die()
    {
        _playerView.StartDeathAnimation();
    }
}

