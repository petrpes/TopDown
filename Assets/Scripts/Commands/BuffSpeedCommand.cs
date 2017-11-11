using UnityEngine;

public class BuffSpeedCommand : Command
{
    [SerializeField] private float _buff;

    public override void ExecuteCommand(GameObject actor)
    {
        WalkableSkillsSet skillSet = actor.GetComponent<WalkableSkillsSet>();
        if (skillSet != null)
        {
            skillSet.Speed += _buff;
        }
    }
}

