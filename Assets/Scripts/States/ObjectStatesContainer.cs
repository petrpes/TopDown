using UnityEngine;

public class ObjectStatesContainer : ObjectStatesContainerBase
{
    [SerializeField] private Mover _mover;
    [SerializeField] private WalkableSkillsSet _skillSet;
    [SerializeField] private float _walkToRunVariance;

    private IStateMachine _walkStateMachine;
    private IStateMachine _attackStateMachine;
    private IStateMachine _healthStateMachine;

    public override IStateMachine WalkStateMachine
    {
        get
        {
            if (_walkStateMachine == null)
            {
                _walkStateMachine = new WalkStateMachine(_mover, _skillSet, _walkToRunVariance);
            }
            return _walkStateMachine;
        }
    }

    public override IStateMachine AttackStateMachine
    {
        get
        {
            if (_attackStateMachine == null)
            {

            }
            return _attackStateMachine;
        }
    }

    public override IStateMachine HealthStateMachine
    {
        get
        {
            if (_healthStateMachine == null)
            {

            }
            return _healthStateMachine;
        }
    }

    private void FixedUpdate()
    {
        SafeUpdate(WalkStateMachine);
        SafeUpdate(AttackStateMachine);
        SafeUpdate(HealthStateMachine);
    }

    private static void SafeUpdate(IStateMachine machine)
    {
        if (machine != null)
        {
            machine.UpdateMachine();
        }
    }
}

