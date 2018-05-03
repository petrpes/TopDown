using UnityEngine;

public interface IWallsCreator
{
    WallsBase CreateWalls(Rect room, Vector2 size, Transform parent);
}

