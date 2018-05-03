using Components.Timer;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SimpleProjectile : Projectile
{
    private DamageSkill _damageSkill;
    private Rigidbody2D _rigidbody2D;
    private ClassInformation _classInformation;
    private ExpirationTimer _timer;

    public override void Shoot(Vector3 position, float rotation, float timeFloat, Vector3 speed, HealthPoints damageAddition,
        Fraction currentFraction)
    {
        if (_rigidbody2D == null)
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
        if (_damageSkill == null)
        {
            _damageSkill = GetComponent<DamageSkill>();
        }
        if (_classInformation == null)
        {
            _classInformation = GetComponent<ClassInformation>();
        }
        if (_timer == null)
        {
            _timer = new ExpirationTimer(timeFloat);
            _timer.OnExpiredTimer += DespawnAction;
        }
        if (_timer.ExpirationTime != timeFloat)
        {
            _timer.ExpirationTime = timeFloat;
        }
        _timer.Start();

        _damageSkill.DamageValue = damageAddition;
        _classInformation.CurrentFraction = currentFraction;

        _rigidbody2D.position = position;
        //_rigidbody2D.rotation = rotation;
        _rigidbody2D.velocity = speed;
    }

    private void DespawnAction()
    {
        ObjectsAPI.DespawnObject(gameObject);
    }
}

