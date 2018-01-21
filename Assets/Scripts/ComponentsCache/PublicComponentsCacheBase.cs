using UnityEngine;

public abstract class PublicComponentsCacheBase : MonoBehaviour
{
    public abstract BuffHandler BuffHandler { get; }
    public abstract Mover Mover { get; }
    public abstract HealthChanger HealthChanger { get; }
    public abstract ClassInformation ClassInformation { get; }
    public abstract ObjectStatesContainerBase StatesContainer { get; }
}

