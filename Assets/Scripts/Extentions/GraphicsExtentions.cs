using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GraphicsExtentions
{
    public static Vector2 GetSpriteSize(this SpriteRenderer spriteRenderer, bool shouldCalculateWithScale = true)
    {
        var spriteSize = spriteRenderer.sprite.GetSize();
        spriteSize.x *= spriteRenderer.size.x;
        spriteSize.y *= spriteRenderer.size.y;

        if (shouldCalculateWithScale)
        {
            var scale = spriteRenderer.gameObject.transform.localScale;
            spriteSize.x *= scale.x;
            spriteSize.y *= scale.y;
        }
        return spriteSize;
    }

    public static Vector2 GetSize(this Sprite sprite)
    {
        return new Vector2(sprite.texture.width, sprite.texture.height) / 
            sprite.pixelsPerUnit;
    }
}

