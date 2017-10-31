using UnityEngine;

[RequireComponent(typeof(HitCommand))]
[RequireComponent(typeof(Rigidbody2D))]
public class SimpleProjectile : Projectile
{
    private HitCommand _hitCommand;
    private Rigidbody2D _rigidbody2D;

    public override void Shoot(Vector3 position, float rotation, Vector3 speed, HealthPoints damageAddition)
    {
        if (_rigidbody2D == null)
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
        if (_hitCommand == null)
        {
            _hitCommand = GetComponent<HitCommand>();
        }

        _hitCommand.Damage = damageAddition;

        _rigidbody2D.position = position;
        _rigidbody2D.rotation = rotation;
        _rigidbody2D.velocity = speed;
    }
}

