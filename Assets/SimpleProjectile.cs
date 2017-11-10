using UnityEngine;

[RequireComponent(typeof(DamageSkill))]
[RequireComponent(typeof(Rigidbody2D))]
public class SimpleProjectile : Projectile
{
    private DamageSkill _damageSkill;
    private Rigidbody2D _rigidbody2D;

    public override void Shoot(Vector3 position, float rotation, float range, Vector3 speed, HealthPoints damageAddition)
    {
        if (_rigidbody2D == null)
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
        if (_damageSkill == null)
        {
            _damageSkill = GetComponent<DamageSkill>();
        }

        _damageSkill.DamageValue = damageAddition;

        _rigidbody2D.position = position;
        _rigidbody2D.rotation = rotation;
        _rigidbody2D.velocity = speed;
    }
}

