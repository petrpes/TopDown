using UnityEngine;

public static class CameraSizeGetter
{
    public static Vector2 GetCameraSize(Camera camera)
    {
        float height = 2f * camera.orthographicSize;
        float width = height * camera.aspect;

        return new Vector2(width, height);
    }
}

