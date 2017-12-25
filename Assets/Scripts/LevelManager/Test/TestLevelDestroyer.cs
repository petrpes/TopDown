using System;

public class TestLevelDestroyer : ILevelDestroyer
{
    public void DestroyCurrentLevel(ILevel level, Action onLevelDestroyed)
    {
        onLevelDestroyed.Invoke();
    }
}

