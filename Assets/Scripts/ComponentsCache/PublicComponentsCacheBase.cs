using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PublicComponentsCacheBase : MonoBehaviour
{
    public abstract BuffHandler BuffHandler { get; }
    public abstract Mover Mover { get; }
    public abstract HealthChanger HealthChanger { get; }
}
