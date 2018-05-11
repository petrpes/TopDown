public interface IBuiltGlobally
{
#if UNITY_EDITOR
    void Build();
#endif
}

