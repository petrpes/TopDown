/// <summary>
/// 
/// </summary>
/// <typeparam name="T">Owner's type</typeparam>
public class StateMachine<T>
{
    private T _owner;
    private IState<T> _currentState;

    public StateMachine(T owner)
    {
        _owner = owner;
        _currentState = null;
    }

    public void ChangeState(IState<T> state)
    {
        if (_currentState != null)
        {
            _currentState.EndState();
        }
        _currentState = state;
        _currentState.StartState(_owner);
    }

    public void Update()
    {
        if (_currentState != null)
        {
            _currentState.UpdateState();
        }
    }
}

