using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[Serializable]
public class ComponentsCache
{
    [SerializeField] private string _componentTypeName;
    [SerializeField] private string _assemblyQualifiedName;
    [SerializeField] private bool _shouldAlsoCountInChildren;
    [SerializeField] private bool _shouldCountSelf = false;
    [SerializeField] private bool _showAdditionalInEditor = false;
    [SerializeField] protected Component[] _components;

    public ComponentsCache(string componentTypeName, bool shouldAlsoCountInChildren, 
        bool shouldAlsoCountSelf = false, string assemblyQualifiedName = "", bool showAdditionalInEditor = false)
    {
        _componentTypeName = componentTypeName;
        _shouldAlsoCountInChildren = shouldAlsoCountInChildren;
        _shouldCountSelf = shouldAlsoCountSelf;
        _assemblyQualifiedName = assemblyQualifiedName;
        _showAdditionalInEditor = showAdditionalInEditor;
    }

    public ComponentsCache(Type type, bool shouldAlsoCountInChildren,
        bool shouldAlsoCountSelf = false)
    {
        _componentTypeName = type.Name;
        _shouldAlsoCountInChildren = shouldAlsoCountInChildren;
        _shouldCountSelf = shouldAlsoCountSelf;
        _assemblyQualifiedName = type.AssemblyQualifiedName;
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

        Type parentType = FindType(_componentTypeName, _assemblyQualifiedName);

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

    public static Type FindType(string typeName, string assemblyQualifiedName)
    {
        Type type = Type.GetType(typeName);

        if (type != null)
        {
            return type;
        }

        if (assemblyQualifiedName != "")
        {
            type = Type.GetType(assemblyQualifiedName);
            if (type != null)
            {
                return type;
            }
        }
        foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
        {
            type = asm.GetType(typeName);
            if (type != null)
            {
                return type;
            }
        }
        return null;
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
    private DrawableProperty _recalculateButton = new DrawableProperty("", "", 30, true, null);

    private DrawableProperty[] _properties;
    protected override DrawableProperty[] Properties
    {
        get
        {
            if (_properties == null)
            {
                var shouldShowAdditional = GetProperty("_showAdditionalInEditor").boolValue;

                _typeName = new DrawableProperty("_componentTypeName", "Type name", 15, shouldShowAdditional, null);
                _countChildren = new DrawableProperty("_shouldAlsoCountInChildren", "Count children", 15, shouldShowAdditional, null);
                _recalculateButton = new DrawableProperty("", "", 20, true, OnButtonPressed);

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

