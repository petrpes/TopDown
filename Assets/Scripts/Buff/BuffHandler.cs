using UnityEngine;

public abstract class BuffHandler : MonoBehaviour
{
    public abstract bool BuffParameter(BuffType type, object value, object sender);
}

public enum BuffType
{
    Speed,
    AccelerationTime,
    Damage,
    ShootDeltaTime
}

