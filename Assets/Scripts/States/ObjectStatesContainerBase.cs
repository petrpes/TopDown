using UnityEngine;

public abstract class ObjectStatesContainerBase : MonoBehaviour
{
    public abstract IStateMachine WalkStateMachine { get; }
    public abstract IStateMachine AttackStateMachine { get; }
    public abstract IStateMachine HealthStateMachine { get; }
}

