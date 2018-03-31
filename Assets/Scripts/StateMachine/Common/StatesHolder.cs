using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StatesHolder
{
    public static class MobMoveStates
    {
        public static readonly IState<MobMoveStateMachine> IdleState = new MobMoveIdleState();
        public static readonly IState<MobMoveStateMachine> AgroState = new MobMoveAgroState();
    }
}

