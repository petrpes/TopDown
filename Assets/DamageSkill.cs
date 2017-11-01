using UnityEngine;

public class DamageSkill : MonoBehaviour
{
    [SerializeField] private HealthPoints _damageValue;

    public HealthPoints DamageValue
    {
        get { return _damageValue; }
        set { _damageValue = value; }
    }
}

