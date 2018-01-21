using UnityEngine;

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
}

