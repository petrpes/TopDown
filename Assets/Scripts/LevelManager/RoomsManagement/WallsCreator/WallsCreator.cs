using UnityEngine;

public class WallsCreator : IWallsCreator
{
    public void CreateWalls(Rect rect, Vector2 size, Transform parent)
    {
        var wall = new GameObject("Walls");
        wall.transform.position = rect.center;
        wall.transform.parent = parent;
        wall.tag = "Wall";
        wall.layer = LayerMask.NameToLayer("Walls");

        var rigidbody = wall.AddComponent<Rigidbody2D>();
        rigidbody.bodyType = RigidbodyType2D.Static;

        for (int i = 0; i < 4; i++)
        {
            var collider = wall.AddComponent<BoxCollider2D>();
            float direction = i < 2 ? 1f : -1f;
            float positionX = i % 2 == 0 ? 0 : rect.width + size.x;
            float positionY = i % 2 == 0 ? rect.height + size.y : 0;
            var colliderPosition = direction * new Vector2(positionX, positionY) / 2f;
            var colliderSize = i % 2 == 0 ?
                                   new Vector2(rect.width + 2f * size.x, size.y) :
                                   new Vector2(size.x, rect.height);

            collider.size = colliderSize;
            collider.offset = colliderPosition;
        }
    }
}

