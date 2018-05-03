using UnityEngine;

public class ParentObjectsComponentCache : PublicComponentsCacheBase
{
    [SerializeField] private PublicComponentsCacheBase _componentsCache;

    public override T GetCachedComponent<T>()
    {
        return _componentsCache.GetCachedComponent<T>();
    }
}

