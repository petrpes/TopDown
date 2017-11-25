public class Level
{
    public Level(IRoomLibrary roomLibrary, int startingRoomId)
    {
        RoomLibrary = roomLibrary;
        StartingRoomId = startingRoomId;
    }

    public IRoomLibrary RoomLibrary { get; private set; }

    public int StartingRoomId { private set; get; }
}

