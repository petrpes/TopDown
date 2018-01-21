using UnityEngine;

public class ParentObjectsComponentCache : PublicComponentsCacheBase
{
    [SerializeField] private PublicComponentCache _componentsCache;

    public override BuffHandler BuffHandler { get { return _componentsCache.BuffHandler; } }

    public override Mover Mover { get { return _componentsCache.Mover; } }

    public override HealthChanger HealthChanger { get { return _componentsCache.HealthChanger; } }

    public override ClassInformation ClassInformation { get { return _componentsCache.ClassInformation; } }

    public override ObjectStatesContainerBase StatesContainer { get { return _componentsCache.StatesContainer; } }
}

