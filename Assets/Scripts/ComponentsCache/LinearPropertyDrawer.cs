using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class LinearPropertyDrawer<T> : PropertyDrawer where T : class
{
    protected abstract DrawableProperty[] Properties { get; }
    protected abstract float Distance { get; }

    protected T BaseObject { get; private set; }
    protected MonoBehaviour MonoBehaviour { get; private set; }
    protected SerializedProperty SerializedProperty { get; private set; }
    protected SerializedProperty GetProperty(string name)
    {
        return SerializedProperty == null ? null : 
            SerializedProperty.FindPropertyRelative(name);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SetBasicObjects(property);

        EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        var visibleProperties = Array.FindAll(Properties, 
            (DrawableProperty) => { return DrawableProperty.IsVisible; });

        if (visibleProperties.Length == 0)
        {
            return;
        }

        Rect[] rects = new Rect[visibleProperties.Length];
        float previousHeight = 0;
        for (int i = 0; i < visibleProperties.Length; i++)
        {
            rects[i] = new Rect(position.x, position.y + previousHeight, position.width, Properties[i].Height);
            previousHeight += Properties[i].Height + Distance;
        }

        int j = 0;
        for (int i = 0; i < Properties.Length; i++)
        {
            if (Properties[i].IsVisible)
            {
                bool isChanged = false;
                SerializedProperty serializedProperty = null;

                if (Properties[i].PropertyName != "")
                {
                    serializedProperty = property.FindPropertyRelative(Properties[i].PropertyName);
                    isChanged = EditorGUI.PropertyField(rects[j],
                        serializedProperty,
                        new GUIContent(Properties[i].Caption));
                }
                else
                {
                    isChanged = GUI.Button(rects[j], Properties[i].Caption);
                }

                if (isChanged && Properties[i].OnChangeAction != null)
                {
                    Properties[i].OnChangeAction.Invoke(serializedProperty);
                }
                j++;
            }
        }

        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        SetBasicObjects(property);

        var visibleProperties = Array.FindAll(Properties,
            (DrawableProperty) => { return DrawableProperty.IsVisible; });
        float height = 0;

        if (visibleProperties.Length == 0)
        {
            return 0;
        }

        foreach (DrawableProperty prop in visibleProperties)
        {
            height += prop.Height;
        }
        height += Distance * (visibleProperties.Length - 1);

        return height;
    }

    private void SetBasicObjects(SerializedProperty property)
    {
        if (SerializedProperty == null)
        {
            SerializedProperty = property;
        }
        if (BaseObject == null)
        {
            BaseObject = fieldInfo.GetValue(property.serializedObject.targetObject) as T;
        }
        if (MonoBehaviour == null)
        {
            MonoBehaviour = property.serializedObject.targetObject as MonoBehaviour;
        }
    }
}

public class DrawableProperty
{
    public string PropertyName { get; private set; }
    public string Caption { get; set; }
    public float Height { get; set; }
    public bool IsVisible { get; set; }
    public Action<SerializedProperty> OnChangeAction { get; set; }

    public DrawableProperty(string propertyName,
                            string caption,
                            float height,
                            bool isVisible,
                            Action<SerializedProperty> onChangeAction)
    {
        PropertyName = propertyName;
        Caption = caption;
        Height = height;
        IsVisible = isVisible;
        OnChangeAction = onChangeAction;
    }
}

