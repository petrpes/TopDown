/// <summary>
/// State's basic interface
/// </summary>
/// <typeparam name="T">Type of state's owner</typeparam>
public interface IState<T>
{
    void StartState(T owner);
    void EndState();
    void UpdateState();
}

