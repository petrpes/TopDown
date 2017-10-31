using UnityEngine;

public class BuffShootDeltaTimeCommand : Command
{
    [SerializeField] private float _buff;

    public override void Execute(GameObject actor)
    {
        actor.GetComponent<MobSkillSet>().ShootDeltaTime /= _buff;
    }
}

