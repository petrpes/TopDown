using UnityEngine;

public interface IDoor
{
    bool IsOpened { get; set; }
    IRoom RoomTo { get; }
    float Width { get; }
    Vector2 DefaultOrientation { get; }
}

