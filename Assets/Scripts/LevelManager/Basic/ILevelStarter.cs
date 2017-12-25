using System;

public interface ILevelStarter
{
    void StartLevel(ILevel level, Action onComplete);
}

