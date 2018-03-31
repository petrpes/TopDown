public class MobMoveAgroState : IState<MobMoveStateMachine>
{
    public void AfterSet(MobMoveStateMachine baseObject)
    {
        baseObject.MoveController.MoveState = MoveStateEnum.Agro;
    }

    public void BeforeReset(MobMoveStateMachine baseObject)
    {
    }

    public IState<MobMoveStateMachine> HandleInput<T>(T input)
    {
        if (input.Equals(false))
        {
            return StatesHolder.MobMoveStates.IdleState;
        }
        return null;
    }
}

