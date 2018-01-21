using UnityEngine;

public class MobSkillSet : MonoBehaviour
{
    [SerializeField] private float _shootDeltaTime;
    [SerializeField] private float _projectileSpeed;

    public float ShootDeltaTime { get { return _shootDeltaTime; } set { _shootDeltaTime = value; } }
    public float ProjectileSpeed { get { return _projectileSpeed; } set { _projectileSpeed = value; } }
}

