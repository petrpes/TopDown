public interface IInterpolator<T>
{
    void AddValue(T value, float time);
    T GetValue(float time);
}

