using UnityEngine;

public class WallsCreator : IWallsCreator
{
    public WallsBase CreateWalls(Rect rect, Vector2 wallsSize, Transform parent)
    {
        var wallGameObject = new GameObject("Walls");
        var wallMono = wallGameObject.AddComponent<Walls>();

        //wallGameObject.transform.position = //rect.center;
        wallGameObject.transform.parent = parent;
        wallGameObject.tag = "Wall";
        wallGameObject.layer = LayerMask.NameToLayer("Walls");

        var rigidbody = wallGameObject.AddComponent<Rigidbody2D>();
        rigidbody.bodyType = RigidbodyType2D.Static;

        for (int i = 0; i < 4; i++)
        {
            var orient = (Orientation)i;
            var roomRect = GeometryExtentions.GetWallRectangle(rect, orient);
            var collider = 
                CollidersExtentions.CreateCollider(wallGameObject, roomRect, false);

            wallMono.AddWall(orient, collider);
        }

        return wallMono;
    }
}

