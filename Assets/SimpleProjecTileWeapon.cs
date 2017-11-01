using System.Collections;
using UnityEngine;

public class SimpleProjecTileWeapon : Weapon
{
    [SerializeField] private Projectile _projecTile;
    [SerializeField] private float _speed;
    [SerializeField] private MobSkillSet _skillSet;
    [SerializeField] private DamageSkill _damageSkill;

    private bool _isCanAttack = true;

    public override bool Attack(Vector3 direction)
    {
        if (_isCanAttack)
        {
            Projectile projecTile = BulletSpawner.Instance.Spawn(_projecTile);
            float rotation = Mathf.Atan2(direction.y, direction.x) *
                Mathf.Rad2Deg - 90;
            projecTile.Shoot(transform.position, rotation, direction * _speed, _damageSkill.DamageValue);
            StartCoroutine(AttackTimer(_skillSet.ShootDeltaTime));

            return true;
        }
        return false;
    }

    private IEnumerator AttackTimer(float deltaTime)
    {
        _isCanAttack = false;
        yield return new WaitForSeconds(deltaTime);
        _isCanAttack = true;
    }
}
