using UnityEngine;

public class BuffAccelerationCommand : Command
{
    [SerializeField] private float _buff;

    public override void ExecuteCommand(GameObject actor)
    {
        WalkableSkillsSet skillSet = actor.GetComponent<WalkableSkillsSet>();
        if (skillSet != null)
        {
            skillSet.AccelerationTime += _buff;
        }
    }
}

