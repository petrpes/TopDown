using UnityEngine;

public class WalkStateMachine : IStateMachine
{
    private Mover _mover;
    private WalkableSkillsSet _skillSet;
    private float _runWalkVariance;

    public WalkStateMachine(Mover mover, WalkableSkillsSet skillSet, float runWalkVariance)
    {
        _mover = mover;
        _skillSet = skillSet;
        _runWalkVariance = runWalkVariance;
    }

    public ObjectState CurrentState { get; private set; }

    public void UpdateMachine()
    {
        if (_mover.MovingSpeed.Equals(Vector3.zero))
        {
            CurrentState = ObjectStateConsts.MoveStates.Idle;
        }
        else if (_mover.WalkingSpeed.Equals(Vector3.zero))
        {
            CurrentState = ObjectStateConsts.MoveStates.Slide;
        }
        else if (_mover.WalkingSpeed.magnitude / _skillSet.Speed >= _runWalkVariance)
        {
            CurrentState = ObjectStateConsts.MoveStates.Run;
        }
        else
        {
            CurrentState = ObjectStateConsts.MoveStates.Walk;
        }
    }
}

