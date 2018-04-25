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

    public static Object ObjectProperty(this Editor editor, string propertyName)
    {
        return editor.serializedObject.FindProperty(propertyName).objectReferenceValue;
    }

    public static T BasicObject<T>(this Editor editor) where T : Component
    {
        return editor.serializedObject.targetObject as T;
    }
}

