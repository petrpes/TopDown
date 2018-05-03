using System;

public interface ILevelDestroyer
{
    void DestroyCurrentLevel(ILevel level, Action onLevelDestroyed);
}

