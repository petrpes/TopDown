using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class ComponentsCache
{
    [SerializeField] private string _componentTypeName;
    [SerializeField] private string _assemblyQualifiedName;
    [SerializeField] private bool _shouldCountInChildren;
    [SerializeField] private bool _shouldCountSelf = false;
    [SerializeField] private bool _showAdditionalInEditor = false;
    [SerializeField] protected Component[] _components;

    public ComponentsCache(string componentTypeName, bool shouldCountInChildren, 
        bool shouldCountSelf = false, string assemblyQualifiedName = "", bool showAdditionalInEditor = false)
    {
        _componentTypeName = componentTypeName;
        _shouldCountInChildren = shouldCountInChildren;
        _shouldCountSelf = shouldCountSelf;
        _assemblyQualifiedName = assemblyQualifiedName;
        _showAdditionalInEditor = showAdditionalInEditor;
    }

    public ComponentsCache(Type type, bool shouldCountInChildren,
        bool shouldAlsoCountSelf = false)
    {
        _componentTypeName = type.Name;
        _shouldCountInChildren = shouldCountInChildren;
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

    public void RecalculateComponents<T>(GameObject parent, Component self) where T : class
    {
        _components = Array.ConvertAll(parent.GetComponentsExtended<T>(_shouldCountInChildren, 
            (comp) => 
            {
                return _shouldCountSelf || (!_shouldCountSelf && comp.Equals(self));
            }), 
            item => item as Component);
    }

    public void RecalculateComponents(GameObject parent, Component self)
    {
        if (_componentTypeName == "")
        {
            return;
        }

        Type parentType = SystemExtentions.FindType(_componentTypeName, _assemblyQualifiedName);

        _components = Array.ConvertAll(parent.GetComponentsExtended(parentType, _shouldCountInChildren,
            (comp) =>
            {
                return !comp.Equals(self) || _shouldCountSelf;
            }),
            item => item as Component);
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
        if (BaseObject != null && MonoComponent != null)
        {
            BaseObject.RecalculateComponents(MonoComponent.gameObject, MonoComponent);
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

