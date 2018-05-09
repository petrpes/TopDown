using System;

public interface ILevelEventListener
{
    Action<ILevel> OnLevelCreated { get; }
    Action<ILevel> OnLevelStarted { get; }
    Action<ILevel> OnLevelDestroyed { get; }
}

