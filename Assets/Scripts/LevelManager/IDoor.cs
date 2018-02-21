public interface IDoor
{
    bool IsOpened { get; set; }
    IRoom RoomTo { get; }
    float Width { get; }
    DoorPosition Position { get; }
}

[System.Serializable]
public struct DoorPosition
{
    public int LineId;
    public float PartOfTheLine;
}

