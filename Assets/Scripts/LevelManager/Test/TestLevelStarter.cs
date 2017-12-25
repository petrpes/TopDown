using System;

public class TestLevelStarter : ILevelStarter
{
    public void StartLevel(ILevel level, Action onComplete)
    {
        LevelManager.Instance.CurrentRoom = level.StartRoom;
        onComplete.Invoke();
    }
}

