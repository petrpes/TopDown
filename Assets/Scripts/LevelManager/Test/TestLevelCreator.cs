using System;

public class TestLevelCreator : ILevelCreator
{
    public void CreateLevel(object levelParams, Action<ILevel> onLevelCreated)
    {
        ILevel newLevel = new TestLevel();

        foreach (IRoom room in newLevel.LevelMap.GetRooms())
        {
            (room as TestRoom).gameObject.SetActive(false);
        }

        onLevelCreated.Invoke(newLevel);
    }
}

