using UnityEngine;

public class MobMoveStateMachine : StateMachine<MobMoveStateMachine>, IObjectAppearanceListener
{
    [SerializeField] private ChangableMoveController _moveController;
    [SerializeField] private Vision _vision;

    public ChangableMoveController MoveController { get { return _moveController; } }

    public void OnAppearanceAction(ObjectAppearanceType type)
    {
        if (type.Equals(ObjectAppearanceType.Appeared))
        {
            CurrentState = StatesHolder.MobMoveStates.IdleState;
        }
        if (type.Equals(ObjectAppearanceType.Created))
        {
            _vision.SpottedImportantObject += OnObjectSpotted;
        }
    }

    private void OnObjectSpotted(GameObject gameObject)
    {
        CurrentState = CurrentState.HandleInput(gameObject != null);
    }
}

