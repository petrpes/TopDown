using Components.Spawner;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtentions
{
    public static HealthChanger GetHealthChanger(this GameObject gameObject)
    {
        var componentsCache = gameObject.GetComponent<PublicComponentsCacheBase>();

        return componentsCache == null ? null : componentsCache.HealthChanger;
    }

    public static Fraction GetFraction(this GameObject gameObject)
    {
        var componentsCache = gameObject.GetComponent<PublicComponentsCacheBase>();

        return componentsCache == null ? Fraction.Neutral :
            componentsCache.ClassInformation == null ? Fraction.Neutral :
            componentsCache.ClassInformation.CurrentFraction;
    }

    public static Mover GetMover(this GameObject gameObject)
    {
        var componentsCache = gameObject.GetComponent<PublicComponentsCacheBase>();

        return componentsCache == null ? null :
            componentsCache.Mover;
    }

    public static ISpawnableObject GetSpawnableObject(this GameObject gameObject)
    {
        var spawnableObject = gameObject.GetComponent<SpawnableObjectsProxy>();
        return spawnableObject;
    }

    public static T[] GetComponentsExtended<T>(this GameObject gameObject, bool shouldCountChildren = false, 
        Predicate<T> predicate = null)
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

