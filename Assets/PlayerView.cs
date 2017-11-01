using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private bool _isLookingLeft;

    public void StartHitAnimation()
    {
        _animator.SetBool("IsHit", true);
    }

    public void StopHitAnimation()
    {
        _animator.SetBool("IsHit", false);
    }

    public void StartDeathAnimation()
    {
        _animator.SetBool("IsDead", true);
    }

    public bool IsLookingLeft
    {
        set
        {
            if (_isLookingLeft != value)
            {
                _isLookingLeft = value;
                _spriteRenderer.flipX = _isLookingLeft;
            }
        }
    }
}
