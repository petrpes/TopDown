using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileView : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void PlayDeathAnimation(Action onStopPlaying)
    {
        _animator.SetBool("IsDead", true);
    }
}
