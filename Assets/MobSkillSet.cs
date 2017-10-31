using UnityEngine;

public class MobSkillSet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private HealthPoints _damage;
    [SerializeField] private float _shootDeltaTime;

    public float Speed { get { return _speed; } set { _speed = value; } }
    public float ShootDeltaTime { get { return _shootDeltaTime; } set { _shootDeltaTime = value; } }
    public HealthPoints Damage { get { return _damage; } set { _damage = value; } }
}

