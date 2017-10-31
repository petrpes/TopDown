using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[Serializable]
public class TagCheckList
{
    [SerializeField] private string[] TagsArray;

    public bool ContainsTag(string tag)
    {
        for (int i = 0; i < TagsArray.Length; i++)
        {
            if (TagsArray[i].Equals(tag))
            {
                return true;
            }
        }
        return false;
    }
}

[CustomPropertyDrawer(typeof(TagCheckList))]
public class TagCheckListEditor : PropertyDrawer
{
    private float CheckBoxSize = 20f;
    private float LabelWidth = 100f;
    private float LabelHeight = 20f;
    private bool isFoldout = true;
    bool isToggled;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //TODO borders
        //isFoldout = EditorGUI.Foldout(position, isFoldout, label.text, true); //TODO foldout

        if (isFoldout)
        {
            EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);

            List<string> tagList = new List<string>();
            tagList.AddRange(UnityEditorInternal.InternalEditorUtility.tags);

            SerializedProperty arrayProperty = property.FindPropertyRelative("TagsArray");
            ResetPropertyList(tagList, arrayProperty);

            for (int i = 0; i < tagList.Count; i++)
            {
                var toggleRect = new Rect(position.x, position.y + CheckBoxSize * i, CheckBoxSize + LabelWidth, CheckBoxSize);

                bool isToggled = ArrayContainsTag(tagList[i], arrayProperty);
                isToggled = EditorGUI.ToggleLeft(toggleRect, tagList[i], isToggled);
                SetListContainsTag(tagList[i], isToggled, arrayProperty);
            }

            var buttonRect = new Rect(position.x, position.y + CheckBoxSize * tagList.Count, CheckBoxSize + LabelWidth, CheckBoxSize);
            if (GUI.Button(buttonRect, "select all"))
            {
                SelectAll(arrayProperty, tagList);
            }

            buttonRect = new Rect(position.x, position.y + CheckBoxSize * (tagList.Count + 1), CheckBoxSize + LabelWidth, CheckBoxSize);
            if (GUI.Button(buttonRect, "deselect all"))
            {
                DeselectAll(arrayProperty);
            }

            EditorGUI.EndProperty();
        }
    }

    private void ResetPropertyList(List<string> tagList, SerializedProperty property)
    {
        int count = property.arraySize;
        List<int> deleteIndexes = new List<int>();

        for (int i = 0; i < count; i++)
        {
            if (!tagList.Contains(property.GetArrayElementAtIndex(i).stringValue))
            {
                deleteIndexes.Add(i);
            }
        }

        for (int i = 0; i < deleteIndexes.Count; i++)
        {
            property.DeleteArrayElementAtIndex(deleteIndexes[i]);
        }
    }

    private void SetListContainsTag(string tag, bool shouldContain, SerializedProperty property)
    {
        int count = property.arraySize;

        for (int i = 0; i < count; i++)
        {
            if (property.GetArrayElementAtIndex(i).stringValue.Equals(tag))
            {
                if (!shouldContain)
                {
                    property.DeleteArrayElementAtIndex(i);
                    count--;
                }
                return;
            }
        }
        if (shouldContain)
        {
            property.InsertArrayElementAtIndex(count);
            property.GetArrayElementAtIndex(count).stringValue = tag;
        }
    }

    private bool ArrayContainsTag(string tag, SerializedProperty property)
    {
        int count = property.arraySize;

        for (int i = 0; i < count; i++)
        {
            if (property.GetArrayElementAtIndex(i).stringValue.Equals(tag))
            {
                return true;
            }
        }
        return false;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float height = base.GetPropertyHeight(property, label);
        if (isFoldout)
        {
            height += UnityEditorInternal.InternalEditorUtility.tags.Length * CheckBoxSize + CheckBoxSize * 2;
        }
        return height;
    }

    private void DeselectAll(SerializedProperty property)
    {
        property.ClearArray();
    }

    private void SelectAll(SerializedProperty property, List<string> tagList)
    {
        DeselectAll(property);

        for (int i = 0; i < tagList.Count; i++)
        {
            property.InsertArrayElementAtIndex(i);
            property.GetArrayElementAtIndex(i).stringValue = tagList[i];
        }
    }
}

