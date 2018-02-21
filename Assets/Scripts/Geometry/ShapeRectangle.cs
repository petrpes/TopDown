using UnityEngine;

public class ShapeRectangle : IShape
{
    private Rect _rectangle;
    private float _perimeter;
    private OrientedLine[] _lines;

    public ShapeRectangle(Rect rectangle)
    {
        _rectangle = rectangle;
        _perimeter = _rectangle.size.x * 2f + _rectangle.size.y * 2f;
        CreateLines();
    }

    public OrientedLine this[int index]
    {
        get
        {
            return _lines[index];
        }
    }

    public Rect Rectangle { get { return _rectangle; } }

    public float Perimeter
    {
        get
        {
            return _perimeter;
        }
    }

    private void CreateLines()
    {
        _lines = new OrientedLine[4];

        _lines[0] = new OrientedLine(new Line(new Vector2(Rectangle.xMin, Rectangle.yMin),
                                     new Vector2(Rectangle.xMin, Rectangle.yMax)), ClockRotation.CounterClockWise);
        _lines[1] = new OrientedLine(new Line(new Vector2(Rectangle.xMin, Rectangle.yMax),
                                     new Vector2(Rectangle.xMax, Rectangle.yMax)), ClockRotation.CounterClockWise);
        _lines[2] = new OrientedLine(new Line(new Vector2(Rectangle.xMax, Rectangle.yMax),
                                     new Vector2(Rectangle.xMax, Rectangle.yMin)), ClockRotation.CounterClockWise);
        _lines[3] = new OrientedLine(new Line(new Vector2(Rectangle.xMax, Rectangle.yMin),
                                     new Vector2(Rectangle.xMin, Rectangle.yMin)), ClockRotation.CounterClockWise);
    }

    public int LinesCount { get { return 4; } }

    public bool IsPointInShape(Vector2 point)
    {
        return Rectangle.Contains(point);
    }

    public override string ToString()
    {
        var result = "";
        for (int i = 0; i < LinesCount; i++)
        {
            result += _lines[i].ToString() + "; ";
        }
        return result;
    }
}

