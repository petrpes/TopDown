using UnityEngine;

public class ComponentsBuffHandler : BuffHandler
{
    [SerializeField] private WalkableSkillsSet _walkableSkillSet;
    [SerializeField] private DamageSkill _damageSkill;
    [SerializeField] private MobSkillSet _mobSkill;

    public override bool BuffParameter(BuffType type, object value, object sender)
    {
        switch (type)
        {
            case BuffType.Speed: _walkableSkillSet.Speed += (float)value; return true;
            case BuffType.AccelerationTime: _walkableSkillSet.AccelerationTime += (float)value; return true;
            case BuffType.Damage: _damageSkill.DamageValue += (HealthPoints)value; return true;
            case BuffType.ShootDeltaTime: _mobSkill.ShootDeltaTime /= (float)value; return true;
        }

        return false;
    }
}

