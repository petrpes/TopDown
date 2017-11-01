using UnityEngine;

public class BuffShootDeltaTimeCommand : Command
{
    [SerializeField] private float _buff;

    public override void ExecuteCommand(GameObject actor)
    {
        actor.GetComponent<MobSkillSet>().ShootDeltaTime /= _buff;
    }
}

