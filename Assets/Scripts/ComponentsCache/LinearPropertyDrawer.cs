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

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        MonoBehaviour = property.serializedObject.targetObject as MonoBehaviour;
        BaseObject = fieldInfo.GetValue(property.serializedObject.targetObject) as T;

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

        for (int i = 0; i < visibleProperties.Length; i++)
        {
            bool isChanged = false;
            SerializedProperty serializedProperty = null;
            if (Properties[i].PropertyName != "")
            {
                serializedProperty = property.FindPropertyRelative(Properties[i].PropertyName);
                isChanged = EditorGUI.PropertyField(rects[i],
                    serializedProperty,
                    new GUIContent(Properties[i].Caption));
            }
            else
            {
                isChanged = GUI.Button(rects[i], Properties[i].Caption);
            }

            if (isChanged && Properties[i].OnChangeAction != null)
            {
                Properties[i].OnChangeAction.Invoke(serializedProperty);
            }
        }

        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
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

