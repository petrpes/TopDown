﻿using Components.Timer;
using UnityEngine;

public class SimpleProjecTileWeapon : Weapon
{
    [SerializeField] private Projectile _projectile;
    [AutomaticSet] [SerializeField] private MobSkillSet _skillSet;
    [AutomaticSet] [SerializeField] private DamageSkill _damageSkill;
    [AutomaticSet] [SerializeField] private ClassInformation _classInformation;

    private bool _isCanAttack = true;
    private ExpirationTimer _shootTimer;

    public override bool Attack(DirectionVector direction)
    {
        if (_shootTimer == null)
        {
            _shootTimer = new ExpirationTimer(_skillSet.ShootDeltaTime);
            _shootTimer.OnExpiredTimer += AttackAction;
        }
        if (_shootTimer.ExpirationTime != _skillSet.ShootDeltaTime)
        {
            _shootTimer.ExpirationTime = _skillSet.ShootDeltaTime;
        }

        if (_isCanAttack)
        {
            Projectile projectile = ObjectsAPI.SpawnObject(_projectile.gameObject).GetComponent<Projectile>();
            float rotation = direction.Value.VectorAngle();
            float projectileSpeed = _skillSet.ProjectileSpeed;

            projectile.Shoot(transform.position, rotation, 1, direction.Value * projectileSpeed, 
                _damageSkill.DamageValue, _classInformation.CurrentFraction);
            _isCanAttack = false;
            _shootTimer.Start();
            return true;
        }
        return false;
    }

    private void AttackAction()
    {
        _isCanAttack = true;
    }
}
