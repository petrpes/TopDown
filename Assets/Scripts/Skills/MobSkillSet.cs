using UnityEngine;

public class MobSkillSet : MonoBehaviour
{
    [SerializeField] private float _shootDeltaTime;

    public float ShootDeltaTime { get { return _shootDeltaTime; } set { _shootDeltaTime = value; } }
}

