using UnityEngine;

public class BuffAccelerationCommand : Command
{
    [SerializeField] private float _buff;

    public override void ExecuteCommand(GameObject actor)
    {
        WalkableSkillsSet skillSet = actor.GetComponent<WalkableSkillsSet>();
        if (skillSet != null)
        {
            Debug.Log(_buff);
            skillSet.AccelerationTime += _buff;
        }
    }
}

