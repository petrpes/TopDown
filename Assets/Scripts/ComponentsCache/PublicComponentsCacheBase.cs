using UnityEngine;

public abstract class PublicComponentsCacheBase : MonoBehaviour
{
    public abstract T GetCachedComponent<T>() where T : class;
}

