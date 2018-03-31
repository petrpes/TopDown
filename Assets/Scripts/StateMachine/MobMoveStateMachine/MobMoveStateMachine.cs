using Components.Spawner;
using UnityEngine;

public class MobMoveStateMachine : StateMachine<MobMoveStateMachine>, ISpawnableObject
{
    [SerializeField] private ChangableMoveController _moveController;
    [SerializeField] private Vision _vision;

    public ChangableMoveController MoveController { get { return _moveController; } }

    public void OnAfterSpawn()
    {
        CurrentState = StatesHolder.MobMoveStates.IdleState;
    }

    public void OnBeforeDespawn()
    {
    }

    private void Awake()
    {
        _vision.SpottedImportantObject += OnObjectSpotted;
    }

    private void OnObjectSpotted(GameObject gameObject)
    {
        CurrentState = CurrentState.HandleInput(gameObject != null);
    }
}

