using Components.Spawner.Pool;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPoolLibrary : IObjectPoolLibrary
{
    private Dictionary<object, object> _poolsDictionary;

    public GameObjectPoolLibrary()
    {
        _poolsDictionary = new Dictionary<object, object>();
    }

    public void AddDependency<T>(T poolableObject, IPool<T> pool)
    {
        var gameObject = Convert(poolableObject);
        _poolsDictionary.Add(gameObject, pool);
    }

    public void Dispose()
    {
        _poolsDictionary = null;
    }

    public IPool<T> GetPool<T>(T poolableObject)
    {
        var gameObject = Convert(poolableObject);
        return _poolsDictionary.SafeGetValue(gameObject) as IPool<T>;
    }

    public void RemoveDependency<T>(T poolableObject)
    {
        var gameObject = Convert(poolableObject);
        _poolsDictionary.SafeRemoveValue(gameObject);
    }

    private GameObject Convert<T>(T obj)
    {
        GameObject gameObject = null;
        if (obj is MonoBehaviour)
        {
            gameObject = (obj as MonoBehaviour).gameObject;
        }
        if (obj is GameObject)
        {
            gameObject = (obj as GameObject);
        }
        if (gameObject == null)
        {
            Debug.LogError("poolableObject should be MonoBehaviour or GameObject");
        }
        return gameObject;
    }
}

