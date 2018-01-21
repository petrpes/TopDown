using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthStateMachine : IStateMachine
{
    private HealthChanger _healthChanger;

    public ObjectState CurrentState { get; private set; }

    public void UpdateMachine()
    {
        CurrentState = ObjectStateConsts.HealthStates.Healthy;
        CurrentState = ObjectStateConsts.HealthStates.Hit;
        CurrentState = ObjectStateConsts.HealthStates.Dead;

    }
}

