using UnityEngine;

public interface IShape
{
    Rect Rectangle { get; }
    bool IsPointInShape(Vector2 point);
    float Perimeter { get; }
    /// <summary>
    /// Lines of the shape where the normale vector should point out of the shape
    /// </summary>
    /// <param name="index">Index of the line</param>
    OrientedLine this[int index] { get; }
    int LinesCount { get; }
}

