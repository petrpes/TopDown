using System;

public interface ILevelCreator
{
    void CreateLevel(Action<Level> onLevelCreated);
}

