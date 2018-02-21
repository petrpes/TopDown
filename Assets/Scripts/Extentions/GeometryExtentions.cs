using System.Collections.Generic;
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

    public static Rect Rotate(this Rect rect)
    {
        var position = new Vector2(rect.xMax, rect.yMin);
        var size = new Vector2(rect.height, rect.width);
        return new Rect();
    }

    public static float ToAngle(this Orientation orientation)
    {
        float angleBetween = 360f / orientation.EnumLength();
        return ((int)orientation - 1) * -angleBetween;
    }

    public static Orientation ToOrientation(this float angle)
    {
        int count = Orientation.Top.EnumLength();
        float angleBetween = 360 / count;

        angle %= 360f;
        if (angle < 0)
        {
            angle = (angle + 360f);
        }

        var anglePart = (int)((360f - angle) / angleBetween);
        return (Orientation)((anglePart + 1) % count);
    }

    public static Vector2 GetPointOnAPerimeter(this IShape shape, int lineId, float linePosition, 
        float lineOffset, out Vector2 normale)
    {
        var line = shape[lineId];
        var lineLine = line.Line;
        lineLine.Length = linePosition;

        normale = line.Normale;
        return lineLine.PointEnd + line.Normale * lineOffset;
    }

    public static Rect ToRect(this OrientedLine line, float width)
    {
        var lineBegin = line.Line + line.Normale * (width / 2f);
        var lineEnd = line.Line + line.Normale * -(width / 2f);

        float xMin = Mathf.Min(lineBegin.PointBegin.x, lineBegin.PointEnd.x, lineEnd.PointEnd.x, lineEnd.PointEnd.x);
        float xMax = Mathf.Max(lineBegin.PointBegin.x, lineBegin.PointEnd.x, lineEnd.PointEnd.x, lineEnd.PointEnd.x);

        float yMin = Mathf.Min(lineBegin.PointBegin.y, lineBegin.PointEnd.y, lineEnd.PointEnd.y, lineEnd.PointEnd.y);
        float yMax = Mathf.Max(lineBegin.PointBegin.y, lineBegin.PointEnd.y, lineEnd.PointEnd.y, lineEnd.PointEnd.y);

        return new Rect(xMin, yMin, xMax - xMin, yMax - yMin);
    }

    public static float AngleBetween(this Vector2 vector1, Vector2 vector2)
    {
        var defaultVector = Vector2.right;
        if (vector1.Equals(Vector2.zero))
        {
            vector1 = defaultVector;
        }
        if (vector2.Equals(Vector2.zero))
        {
            vector2 = defaultVector;
        }

        /*
        var value = vector1.x * vector2.x + vector1.y * vector2.y;
        value /= vector1.magnitude * vector2.magnitude;
        */
        return Vector2.SignedAngle(vector1, vector2);
    }
}

public enum Orientation : byte
{
    Top = 0,
    Right = 1,
    Bottom = 2,
    Left = 3
}

public enum ClockRotation : byte
{
    ClockWise = 0,
    CounterClockWise = 1
}

public struct Line
{
    public Vector2 PointBegin;
    public Vector2 PointEnd;

    public float Length
    {
        get { return ToVector.magnitude; }
        set
        {
            var vector = ToVector * (value / ToVector.magnitude);
            PointEnd = PointBegin + vector;
        }
    }

    public void Expand(float newLength)
    {
        var lengthAddition = (newLength - Length) / 2f;
        var vectorAddition = ToVector.normalized * lengthAddition;

        PointBegin -= vectorAddition;
        PointEnd += vectorAddition;
    }

    public Line(Vector2 point1, Vector2 point2)
    {
        PointBegin = point1;
        PointEnd = point2;
    }

    public bool Contains(Vector2 point)
    {
        return (point.x - PointBegin.x) / (PointEnd.x - PointBegin.x) ==
            (point.y - PointBegin.y) / (PointEnd.y - PointBegin.y);
    }

    public void CutLine(float position, out Line line1, out Line line2)
    {
        line1 = this;
        line1.Length = position;

        line2 = new Line(line1.PointEnd, PointEnd);
    }

    public Vector2 ToVector
    {
        get
        {
            return new Vector2(PointEnd.x - PointBegin.x, PointEnd.y - PointBegin.y);
        }
    }

    public Vector2 Center
    {
        get
        {
            return (PointBegin + PointEnd) / 2f;
        }
    }

    public static Line operator +(Line line, Vector2 offset)
    {
        return new Line(line.PointBegin + offset, line.PointEnd + offset);
    }

    public override string ToString()
    {
        return PointBegin.ToString() + " | " + PointEnd.ToString();
    }
}

public struct OrientedLine
{
    public readonly Line Line;
    public readonly Vector2 Normale;
    public readonly ClockRotation NormaleRotation;

    /// <summary>
    /// Line with the normale vector
    /// </summary>
    /// <param name="line">Line</param>
    /// <param name="normaleRotation">How the normal is rotated relative to the line</param>
    public OrientedLine(Line line, ClockRotation normaleRotation)
    {
        Line = line;
        NormaleRotation = normaleRotation;

        var vector = line.ToVector;
        Normale = (
                    normaleRotation == ClockRotation.ClockWise ? 
                    new Vector2(vector.y, -vector.x) : 
                    new Vector2(-vector.y, vector.x)
                  ).normalized;
    }

    public override string ToString()
    {
        return "Line = " + Line.ToString() + "; Normale = " + Normale.ToString();
    }
}

