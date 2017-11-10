using UnityEngine;

public class WalkableSkillsSet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _accelerationTime;

    public float Speed { get { return _speed; } set { _speed = value; } }
    public float AccelerationTime { get { return _accelerationTime; } set { _accelerationTime = value; } }
}

