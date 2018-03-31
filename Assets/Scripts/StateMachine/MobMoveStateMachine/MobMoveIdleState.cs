using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMoveIdleState : IState<MobMoveStateMachine>
{
    public void AfterSet(MobMoveStateMachine baseObject)
    {
        baseObject.MoveController.MoveState = MoveStateEnum.Idle;
    }

    public void BeforeReset(MobMoveStateMachine baseObject)
    {
    }

    public IState<MobMoveStateMachine> HandleInput<T>(T input)
    {
        if (input.Equals(true))
        {
            return StatesHolder.MobMoveStates.AgroState;
        }
        return null;
    }
}

