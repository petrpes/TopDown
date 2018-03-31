using UnityEngine;

public static class ObjectExtentions
{
    public static bool SafeEquals<T>(this T object1, T object2) where T : class
    {
        return ((object1 == null && object2 == null) ||
                (object1 != null && object1.Equals(object2)) ||
                (object2 != null && object2.Equals(object1)));
    }
}

