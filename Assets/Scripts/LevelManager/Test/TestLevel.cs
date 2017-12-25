public class TestLevel : ILevel
{
    private IRoom _startRoom;

    public IRoom StartRoom
    {
        get
        {
            if (_startRoom == null)
            {
                foreach (IRoom room in LevelMap.GetRooms())
                {
                    _startRoom = room;
                    break;
                }
            }
            return _startRoom;
        }
    }

    public ILevelMap LevelMap
    {
        get
        {
            return TestLevelMap.Instance;
        }
    }
}

