using System.Collections.Generic;
using UnityEngine;

public class WallsColliderCreator : IWallsColliderCreator
{
    private class DoorPositionPair
    {
        public IDoor Door;
        public DoorPosition Position;
    }

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
        var doorsSortList = new List<DoorPositionPair>(10);

        for (int i = 0; i < shape.LinesCount; i++)
        {
            var line = shape[i];
            var lineLine = line.Line + line.Normale * wallWidth / 2f;
            lineLine.Expand(lineLine.Length + wallWidth * 2f);

            var offsetLine = new OrientedLine(lineLine, line.NormaleRotation);

            if (holder != null)
            {
                foreach (var door in holder.GetDoors((door, doorPosition) => { return doorPosition.LineId == i; }))
                {
                    doorsSortList.Add(new DoorPositionPair() { Door = door, Position = holder.GetDoorPosition(door) });
                }
            }

            if (doorsSortList.Count == 0)
            {
                wallRects.Add(offsetLine.ToRect(wallWidth));
            }
            else
            {
                doorsSortList.Sort(delegate (DoorPositionPair x, DoorPositionPair y) 
                    { return x.Position.PartOfTheLine < y.Position.PartOfTheLine ? -1 : 1; });

                var lineNew = offsetLine.Line;
                var positionOffset = wallWidth;

                foreach (var doorSorted in doorsSortList)
                {
                    var doorWidth = doorSorted.Door.Width;
                    var newPosition = doorSorted.Position.PartOfTheLine +
                        positionOffset;
                    Line line1;
                    Line line2;

                    lineNew.CutLine(newPosition - doorWidth / 2f, out line1, out line2);

                    wallRects.Add(new OrientedLine(line1, line.NormaleRotation).ToRect(wallWidth));

                    lineNew.CutLine(newPosition + doorWidth / 2f, out line1, out line2);
                    lineNew = line2;
                }

                wallRects.Add(new OrientedLine(lineNew, line.NormaleRotation).ToRect(wallWidth));
                doorsSortList.Clear();
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

