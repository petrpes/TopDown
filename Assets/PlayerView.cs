using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Animation _hitAnimation;
    [SerializeField] private Animation _deathAnimation;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private bool _isLookingLeft;

    public void StartHitAnimation()
    {
        _hitAnimation.Play();
    }

    public void StopHitAnimation()
    {
        _hitAnimation.Stop();
    }

    public void StartDeathAnimation()
    {
        _deathAnimation.Play();
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
