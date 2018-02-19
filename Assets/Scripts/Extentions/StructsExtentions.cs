using System;
using System.Collections.Generic;

public static class StructsExtentions
{
    public static V SafeGetValue<K, V>(this Dictionary<K, V> dictionary, K key) where V : class
    {
        if (dictionary.ContainsKey(key))
        {
            return dictionary[key];
        }
        return null;
    }

    public static void SafeRemoveValue<K, V>(this Dictionary<K, V> dictionary, K key) where V : class
    {
        if (dictionary.ContainsKey(key))
        {
            dictionary.Remove(key);
        }
        return;
    }

    public static int EnumLength<T>(this T obj) where T : struct, IConvertible
    {
        return Enum.GetValues(typeof(T)).Length;
    }

    public static int EnumLength<T>() where T : struct, IConvertible
    {
        return Enum.GetValues(typeof(T)).Length;
    }
}

