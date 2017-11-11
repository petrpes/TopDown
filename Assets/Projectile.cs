using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public abstract void Shoot(Vector3 position, float rotation, float length, Vector3 speed, HealthPoints damageAddition, 
        Fraction currentFraction);
}

