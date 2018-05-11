using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class UnityEditorExtentions
{
    public static SerializedProperty Property(this Editor editor, string propertyName)
    {
        return editor.serializedObject.FindProperty(propertyName);
    }

    public static UnityEngine.Object ObjectProperty(this Editor editor, string propertyName)
    {
        return editor.serializedObject.FindProperty(propertyName).objectReferenceValue;
    }

    public static T BasicComponent<T>(this Editor editor) where T : Component
    {
        return editor.serializedObject.targetObject as T;
    }

    public static void ChooseComponentWithWindow<T>(this GameObject parent,
        Action<T> onSuccess, Action onFailure = null) where T : class
    {
        SelectComponentsDialogWindow.ShowWindow("Select component",
            "GameObject " + parent.name + " has multiple components of type " + typeof(T).Name +
            " in it's children. Please select suitable.",
            parent.GetComponentsExtended<T>(true),
            onSuccess, onFailure);
    }

    public static void ChooseComponentWithWindow(this GameObject parent, Type componentType,
        Action<Component> onSuccess, Action onFailure = null)
    {
        SelectComponentsDialogWindow.ShowWindow("Select component",
            "GameObject " + parent.name + " has multiple components of type " + componentType.Name +
            " in it's children. Please select suitable.",
            parent.GetComponentsExtended(componentType, true),
            onSuccess, onFailure);
    }
}

