using UnityEngine;

public static class RoomConsts
{
    public static float WallsWidth = 1;
    public static Vector2 WallsSize = new Vector2(WallsWidth, WallsWidth);
    public static float DoorsWidth = 1;
    public static bool[] ColliderOversize = { true, false, true, false };
    public static Vector2 VisibleEdgeSize = new Vector2(0.5f, 0.5f);
}

