﻿using UnityEngine;

public class BuffSpeedCommand : Command
{
    [SerializeField] private float _buff;

    public override void ExecuteCommand(GameObject actor)
    {
        actor.GetComponent<WalkableSkillsSet>().Speed += _buff;
    }
}
