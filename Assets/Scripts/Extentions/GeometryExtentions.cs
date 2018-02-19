using UnityEngine;
using Axis = UnityEngine.RectTransform.Axis;

public static class GeometryExtentions
{
    public static Vector2 Multiply(this Vector2 vector, Vector2 variance)
    {
        return new Vector2(vector.x * variance.x, vector.y * variance.y);
    }

    public static float VectorAngle(this Vector3 vector)
    {
        return Mathf.Atan2(vector.y, vector.x) *
                Mathf.Rad2Deg - 90;
    }

    public static float VectorAngle(this Vector2 vector)
    {
        return Mathf.Atan2(vector.y, vector.x) *
                Mathf.Rad2Deg - 90;
    }

    public static Quaternion RotationTowards(this Vector3 vector)
    {
        return Quaternion.Euler(0f, 0f, vector.VectorAngle());
    }

    public static Vector3 RandomVector(float length = 1)
    {
        return new Vector3(
                            Random.Range(-length, length),
                            Random.Range(-length, length),
                            Random.Range(-length, length)
                          );
    }

    public static Rect CreateRect(Vector2 centralPoint, Vector2 size)
    {
        var rect = new Rect(new Vector2(), size);
        rect.center = centralPoint;
        return rect;
    }

    public static Axis ToAxis(this Orientation orientation)
    {
        return orientation == Orientation.Bottom || orientation == Orientation.Top ?
                    Axis.Horizontal :
                    Axis.Vertical;
    }

    public static Rect GetWallRectangle(Rect roomRect, Orientation orientation)
    {
        var axis = orientation.ToAxis();
        var wallsSize = RoomConsts.WallsSize;
        var oversize = RoomConsts.ColliderOversize[(int)orientation];

        float xMin = 0;
        float yMin = 0;
        float width = 0;
        float height = 0;
        float oversizeValue = oversize ? 1 : 0;

        switch (orientation)
        {
            case Orientation.Top:
                xMin = roomRect.xMin - wallsSize.x * oversizeValue;
                yMin = roomRect.yMax;
                width = roomRect.width + wallsSize.x * 2f * oversizeValue;
                height = wallsSize.y;
                break;
            case Orientation.Right:
                xMin = roomRect.xMax;
                yMin = roomRect.yMin - wallsSize.y * oversizeValue;
                width = wallsSize.x;
                height = roomRect.height + wallsSize.y * 2f * oversizeValue;
                break;
            case Orientation.Bottom:
                xMin = roomRect.xMin - wallsSize.x * oversizeValue;
                yMin = roomRect.yMin - wallsSize.y;
                width = roomRect.width + wallsSize.x * 2f * oversizeValue;
                height = wallsSize.y;
                break;
            case Orientation.Left:
                xMin = roomRect.xMin - wallsSize.x;
                yMin = roomRect.yMin - wallsSize.y * oversizeValue;
                width = wallsSize.x;
                height = roomRect.height + wallsSize.y * 2f * oversizeValue;
                break;
        }

        return new Rect(new Vector2(xMin, yMin), new Vector2(width, height));
    }

    public static Rect Rotate(this Rect rect)
    {
        var position = new Vector2(rect.xMax, rect.yMin);
        var size = new Vector2(rect.height, rect.width);
        return new Rect();
    }
}

public struct AxisLine
{
    public Axis Axis { get; set; }
    public Vector2 StartPosition { get; set; }
    public float Length { get; set; }

    public AxisLine(Axis axis, Vector2 startPosition, float length)
    {
        Axis = axis;
        StartPosition = startPosition;
        Length = length;
    }

    public Vector2 StartPoint
    {
        get
        {
            return StartPosition;
        }
    }

    public Vector2 EndPoint
    {
        get
        {
            return AddLength(Length);
        }
    }

    private Vector2 AddLength(float length)
    {
        var positionOffset = Axis == Axis.Horizontal ?
                             new Vector2(length, 0) :
                             new Vector2(0, length);
        return StartPosition + positionOffset;
    }

    public AxisLine CutLine(float position, float length)
    {
        var positionOffset = AddLength(position) ;
        var newStartPosition = StartPosition + positionOffset;
        return new AxisLine(Axis, newStartPosition, length);
    }

    public bool IsPointOnLine(float position)
    {
        return Axis == Axis.Horizontal ?
               StartPoint.x >= position && EndPoint.x <= position :
               StartPoint.y >= position && EndPoint.y <= position;
    }
}

public struct AxisRect
{
    public AxisLine AxisLine { get; set; }
    public float Width { get; set; }

    public AxisRect(AxisLine axisLine, float width)
    {
        AxisLine = axisLine;
        Width = width;
    }
    /*
    public AxisRect(Axis axis, Rect rect)
    {
        if (axis == Axis.Vertical)
        {
            rect = rect.Rotate();
        }

        AxisLine = new AxisLine(axis, new Vector2(rect.xMin, rect.center.y));
    }*/

    public Rect Rectangle
    {
        get
        {
            var wallsSize = RoomConsts.WallsSize;
            var vectorOffset = AxisLine.Axis == Axis.Horizontal ?
                               new Vector2(0, wallsSize.y / 2f) :
                               new Vector2(wallsSize.x / 2f, 0);
            return new Rect(AxisLine.StartPoint - vectorOffset,
                            AxisLine.EndPoint + vectorOffset);
        }
    }
}

public enum Orientation : byte
{
    Top = 0,
    Right = 1,
    Bottom = 2,
    Left = 3
}

