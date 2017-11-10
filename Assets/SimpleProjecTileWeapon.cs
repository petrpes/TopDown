using Components.Timer;
using UnityEngine;

public class SimpleProjecTileWeapon : Weapon
{
    [SerializeField] private Projectile _projecTile;
    [SerializeField] private float _speed;
    [SerializeField] private MobSkillSet _skillSet;
    [SerializeField] private DamageSkill _damageSkill;

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
            Projectile projecTile = BulletSpawner.Instance.Spawn(_projecTile);
            float rotation = Mathf.Atan2(direction.Value.y, direction.Value.x) *
                Mathf.Rad2Deg - 90;
            projecTile.Shoot(transform.position, rotation, 1, direction.Value * _speed, _damageSkill.DamageValue);
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
