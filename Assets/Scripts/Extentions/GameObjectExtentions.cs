﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtentions
{
    /*
    public static HealthChanger GetHealthChanger(this GameObject gameObject)
    {
        //if (gameObject.pare)
    }
    */

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
}

