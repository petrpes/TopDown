public interface IDoor
{
    float Width { get; }
    bool IsOpened { get; set; }
    IRoom RoomTo { get; }
    Orientation Orientation { get; }
}

