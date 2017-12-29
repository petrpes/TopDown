using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InterfaceComponentCache
{
    [SerializeField] private Component _component;

    public T GetChachedComponent<T>() where T : class
    {
        if (_component is T)
        {
            return _component as T;
        }
        return null;
    }

    public void SetValue<T>(T value) where T : class
    {
        if (value is Component)
        {
            _component = value as Component;
        }
    }
}

