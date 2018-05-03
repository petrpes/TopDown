using System;

public class TestLevelStarter : ILevelStarter
{
    public void StartLevel(ILevel level, Action onComplete)
    {
        LevelAPIs.ChangeRoom(level.StartRoom);
        onComplete.Invoke();
    }
}

