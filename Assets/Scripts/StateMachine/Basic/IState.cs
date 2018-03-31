public interface IState<M>
{
    void AfterSet(M baseObject);
    void BeforeReset(M baseObject);
    IState<M> HandleInput<T>(T input);
}

