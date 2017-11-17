using System.Collections;
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
        ClassInformation actorClassInformation = gameObject.GetComponent<ClassInformation>();
        return actorClassInformation == null ? Fraction.Neutral :
            actorClassInformation.CurrentFraction;
    }
}

