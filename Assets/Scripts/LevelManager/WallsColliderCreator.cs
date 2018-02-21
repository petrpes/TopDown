using System.Collections.Generic;
using UnityEngine;

public class WallsColliderCreator : IWallsColliderCreator
{
    public bool CreateColliders(IShape shape, IDoorsHolder holder, GameObject gameObject)
    {
        if (shape is ShapeRectangle)
        {
            return CreateRectCollider((ShapeRectangle)shape, holder, gameObject);
        }

        return false;
    }

    private bool CreateRectCollider(ShapeRectangle shape, IDoorsHolder holder, GameObject gameObject)
    {
        var wallRects = new List<Rect>();
        var wallWidth = RoomConsts.WallsWidth;
        var doorsSortList = new List<IDoor>(10);

        for (int i = 0; i < shape.LinesCount; i++)
        {
            var line = shape[i];
            var lineLine = line.Line + line.Normale * wallWidth / 2f;
            lineLine.Expand(lineLine.Length + wallWidth * 2f);

            var offsetLine = new OrientedLine(lineLine, line.NormaleRotation);

            if (holder == null)
            {
                wallRects.Add(offsetLine.ToRect(wallWidth));
            }
            else
            {
                doorsSortList.InsertRange(0, holder.GetDoors((door) => 
                    { return door.Position.LineId == i; }));
                doorsSortList.Sort(delegate (IDoor x, IDoor y) 
                    { return x.Position.PartOfTheLine < y.Position.PartOfTheLine ? -1 : 1; });

                foreach (var d in doorsSortList)
                {
                    Debug.Log(d.Position.PartOfTheLine);
                }
                return true;

                var lineNew = offsetLine.Line;
                var positionOffset = wallWidth;

                foreach (var doorSorted in doorsSortList)
                {
                    var doorWidth = doorSorted.Width;
                    var newPosition = doorSorted.Position.PartOfTheLine +
                        positionOffset;
                    Line line1;
                    Line line2;

                    lineNew.CutLine(newPosition - doorWidth / 2f, out line1, out line2);

                    wallRects.Add(new OrientedLine(line1, line.NormaleRotation).ToRect(wallWidth));

                    lineNew.CutLine(newPosition + doorWidth / 2f, out line1, out line2);
                    lineNew = line2;
                }
            }
        }

        foreach (var rect in wallRects)
        {
            var collider = gameObject.AddComponent<BoxCollider2D>();
            collider.usedByComposite = true;
            collider.offset = rect.center;
            collider.size = rect.size;
        }

        gameObject.AddComponent<CompositeCollider2D>();
        return true;
    }
}

