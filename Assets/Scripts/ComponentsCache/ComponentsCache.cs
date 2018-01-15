using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[Serializable]
public class ComponentsCache
{
    [SerializeField] private string _componentTypeName;
    [SerializeField] private bool _shouldAlsoCountInChildren;
    [SerializeField] private bool _shouldCountSelf = false;
    [SerializeField] protected Component[] _components;

    public ComponentsCache(string componentTypeName, bool shouldAlsoCountInChildren, 
        bool shouldAlsoCountSelf = false)
    {
        _componentTypeName = componentTypeName;
        _shouldAlsoCountInChildren = shouldAlsoCountInChildren;
        _shouldCountSelf = shouldAlsoCountSelf;
    }

    public int Count { get { return _components == null ? 0 : _components.Length; } }

    public IEnumerable<T> GetCachedComponets<T>() where T : class
    {
        if (_components == null)
        {
            yield return null;
        }

        for (int i = 0; i < _components.Length; i++)
        {
            if (_components[i] is T)
            {
                yield return _components[i] as T;
            }
        }
    }

    public void RecalculateComponents<T>(GameObject parent, Component self)
    {
        if (_shouldAlsoCountInChildren)
        {
            _components = Array.ConvertAll(parent.GetComponentsInChildren<T>(), item => item as Component);
        }
        else
        {
            _components = Array.ConvertAll(parent.GetComponents<T>(), item => item as Component);
        }

        if (self is T)
        {
            RemoveSelf(self);
        }
    }

    public void RecalculateComponents(GameObject parent, Component self)
    {
        if (_componentTypeName == "")
        {
            return;
        }

        Type parentType = Type.GetType(_componentTypeName);

        if (_shouldAlsoCountInChildren)
        {
            _components = Array.ConvertAll(parent.GetComponentsInChildren(parentType), item => item as Component);
        }
        else
        {
            _components = Array.ConvertAll(parent.GetComponents(parentType), item => item as Component);
        }

        RemoveSelf(self);
    }

    private void RemoveSelf(Component self)
    {
        if (_components == null || self == null || _shouldCountSelf)
        {
            return;
        }

        _components =
            _components.Where((component) =>
            { return !component.Equals(self); }).ToArray();
    }
}

[CustomPropertyDrawer(typeof(ComponentsCache))]
public class ComponentsCacheEditor : LinearPropertyDrawer<ComponentsCache>
{
    private DrawableProperty _typeName = new DrawableProperty("_componentTypeName", "Type name", 15, true, null);
    private DrawableProperty _countChildren = new DrawableProperty("_shouldAlsoCountInChildren", "Count children", 15, true, null);
    private DrawableProperty _recalculateButton = new DrawableProperty("", "", 20, true, null);

    private DrawableProperty[] _properties;
    protected override DrawableProperty[] Properties
    {
        get
        {
            if (_properties == null)
            {
                _recalculateButton.OnChangeAction = OnButtonPressed;
                _properties = new DrawableProperty[] { _typeName, _countChildren, _recalculateButton };
            }
            return _properties;
        }
    }

    protected override float Distance
    {
        get { return 5f; }
    }

    private void OnButtonPressed(SerializedProperty property)
    {
        if (BaseObject != null && MonoBehaviour != null)
        {
            BaseObject.RecalculateComponents(MonoBehaviour.gameObject, MonoBehaviour);
        }
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        _recalculateButton.Caption = "Recalculate (Count = " + ObjectsCount + ")";

        base.OnGUI(position, property, label);
    }

    private int ObjectsCount
    {
        get
        {
            return BaseObject == null ? 0 : BaseObject.Count;
        }
    }
}

