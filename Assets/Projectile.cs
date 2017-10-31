using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public abstract void Shoot(Vector3 position, float rotation, Vector3 speed, HealthPoints damageAddition);
}

