using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class ComponentsCache
{
    [SerializeField] private string _componentTypeName;
    [SerializeField] private bool _shouldAlsoCountInChildren;
    [SerializeField] protected Component[] _components;

    public ComponentsCache(string componentTypeName, bool shouldAlsoCountInChildren)
    {
        _componentTypeName = componentTypeName;
        _shouldAlsoCountInChildren = shouldAlsoCountInChildren;
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

    public void RecalculateComponents<T>(GameObject parent, bool shouldAlsoCountInChildren)
    {
        if (shouldAlsoCountInChildren)
        {
            _components = Array.ConvertAll(parent.GetComponentsInChildren<T>(), item => item as Component);
        }
        else
        {
            _components = Array.ConvertAll(parent.GetComponents<T>(), item => item as Component);
        }
    }

    public void RecalculateComponents(GameObject parent)
    {
        if (_componentTypeName == "")
        {
            return;
        }

        if (_shouldAlsoCountInChildren)
        {
            _components = Array.ConvertAll(parent.GetComponentsInChildren(Type.GetType(_componentTypeName)), item => item as Component);
        }
        else
        {
            _components = Array.ConvertAll(parent.GetComponents(Type.GetType(_componentTypeName)), item => item as Component);
        }
    }
}

[CustomPropertyDrawer(typeof(ComponentsCache))]
public class ComponentsCacheEditor : PropertyDrawer
{
    private float _textHeight = 15;
    private float _checkHeight = 15;
    private float _buttonHeight = 20;
    private float _distance = 5;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //base.OnGUI(position, property, label);

        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects
        var typeNameRect = new Rect(position.x, position.y, 
            position.width, _textHeight);
        var boolRect =     new Rect(position.x, position.y + _textHeight + _distance, 
            position.width, _checkHeight);
        var buttonRect =   new Rect(position.x, position.y + _textHeight + _checkHeight + _distance * 2, 
            position.width, _buttonHeight);

        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(typeNameRect, property.FindPropertyRelative("_componentTypeName"), new GUIContent("Type name"));
        EditorGUI.PropertyField(boolRect, property.FindPropertyRelative("_shouldAlsoCountInChildren"), new GUIContent("Count children"));

        var componentsCache = 
            fieldInfo.GetValue(property.serializedObject.targetObject) as ComponentsCache;
        var count = componentsCache.Count;

        if (GUI.Button(buttonRect, "Recalculate (Count = "+ count + ")"))
        {
            var monoBehaviour = property.serializedObject.targetObject as MonoBehaviour;
            componentsCache.RecalculateComponents(monoBehaviour.gameObject);
        }

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return _textHeight + _buttonHeight + _checkHeight + _distance * 2;
    }
}

