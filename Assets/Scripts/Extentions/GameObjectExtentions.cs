using System;
using UnityEngine;

public static class GameObjectExtentions
{
    public static T GetLevelObjectComponent<T>(this object roomObject) where T : class
    {
        if (typeof(T).Equals(typeof(PublicComponentsCacheBase)))
        {
            if (roomObject is PublicComponentsCacheBase)
            {
                return roomObject as T;
            }
            if (roomObject is GameObject)
            {
                return (roomObject as GameObject).GetComponent<PublicComponentsCacheBase>() as T;
            }
            return null;
        }

        var componentsCache = roomObject.GetLevelObjectComponent<PublicComponentsCacheBase>();
        if (componentsCache == null)
        {
            return null;
        }

        return componentsCache.GetCachedComponent<T>();
    }

    public static Fraction GetFraction(this PublicComponentsCacheBase componentCache)
    {
        var componentsCache = componentCache.GetLevelObjectComponent<PublicComponentsCacheBase>();
        if (componentsCache == null)
        {
            return Fraction.Neutral;
        }
        var classInfo = componentsCache.GetCachedComponent<ClassInformation>();

        return classInfo == null ? Fraction.Neutral :
            classInfo.CurrentFraction;
    }

    public static Fraction GetFraction(this GameObject gameObject)
    {
        var cache = gameObject.GetLevelObjectComponent<PublicComponentsCacheBase>();
        if (cache == null)
        {
            return Fraction.Neutral;
        }
        return cache.GetFraction();
    }

    public static T GetComponentExtended<T>(this GameObject gameObject, bool shouldCountChildren = false,
        Predicate<T> predicate = null, bool showWarningOnMoreThanOne = false, bool returnNullOnMoreThanOne = false)
        where T : class
    {
        T[] result = gameObject.GetComponentsExtended(shouldCountChildren, predicate);

        if (result == null || result.Length == 0)
        {
            return null;
        }
        else if (result.Length > 1)
        {
            if (showWarningOnMoreThanOne)
            {
                Debug.LogWarning("There are more than one component of type " + typeof(T).Name);
            }
            if (returnNullOnMoreThanOne)
            {
                return null;
            }
        }
        return result[0];
    }

    public static T[] GetComponentsExtended<T>(this GameObject gameObject, bool shouldCountChildren = false, 
        Predicate<T> predicate = null) where T : class
    {
        T[] result;

        if (shouldCountChildren)
        {
            result = gameObject.GetComponentsInChildren<T>();
        }
        else
        {
            result = gameObject.GetComponents<T>();
        }

        if (result != null && predicate != null)
        {
            result = Array.FindAll(result, predicate);
        }

        return result;
    }

    public static Component[] GetComponentsExtended(this GameObject gameObject, Type componentType,
        bool shouldCountChildren = false, Predicate<Component> predicate = null)
    {
        Component[] result;

        if (shouldCountChildren)
        {
            result = gameObject.GetComponentsInChildren(componentType);
        }
        else
        {
            result = gameObject.GetComponents(componentType);
        }

        if (result != null && predicate != null)
        {
            result = Array.FindAll(result, predicate);
        }

        return result;
    }
}

