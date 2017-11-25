using System;

public interface ILevelDestroyer
{
    void DestroyLevel(Level level, Action onLevelDestroyed);
}

