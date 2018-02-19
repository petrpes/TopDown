using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CollidersExtentions
{
    public static Collider2D CreateCollider(this GameObject gameObject, 
                                            Rect rectangle,
                                            bool isTrigger)
    {
        var collider = gameObject.AddComponent<BoxCollider2D>();

        collider.isTrigger = isTrigger;
        collider.size = rectangle.size;
        collider.offset = rectangle.position;

        return collider;
    }
}

