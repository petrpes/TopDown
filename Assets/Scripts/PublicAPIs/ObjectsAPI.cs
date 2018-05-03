using UnityEngine;

public static class ObjectsAPI
{
    public static bool DespawnObject(GameObject gameObject)
    {
        return SceneObjectsMananger.Instance.SpawnManager.Despawn(gameObject);
    }

    public static GameObject SpawnObject(GameObject prefab)
    {
        return SceneObjectsMananger.Instance.SpawnManager.Spawn(prefab);
    }

    public static IObjectAppearanceHooks<object> Hooks
    {
        get
        {
            return SceneObjectsMananger.Instance.AppearanceHooks;
        }
    }
}

