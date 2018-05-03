using System;

public interface ILevelCreator
{
    void CreateLevel(object levelParams, Action<ILevel> onLevelCreated);
}

