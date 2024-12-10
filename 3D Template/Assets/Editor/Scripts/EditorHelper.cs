using UnityEditor;
using UnityEngine;

public static class EditorHelper
{
    public static void DrawProperty(ref Rect position, SerializedProperty property, string relativePropertyPath, bool displayName = true, string nameToDisplay = "")
    {
        SerializedProperty prop = property.FindPropertyRelative(relativePropertyPath);

        if (displayName)
            EditorGUI.PropertyField(new Rect(position.position, new(position.width, EditorGUIUtility.singleLineHeight)), prop);
        else
            EditorGUI.PropertyField(new Rect(position.position, new(position.width, EditorGUIUtility.singleLineHeight)), prop, new GUIContent(nameToDisplay));

        position.y += EditorGUI.GetPropertyHeight(prop) + 2;
    }

    public static void DrawProperties(Rect position, SerializedProperty property, params string[] relativePropertyPath)
    {
        foreach (string propertyPath in relativePropertyPath)
        {
            SerializedProperty prop = property.FindPropertyRelative(propertyPath);

            float height = EditorGUI.GetPropertyHeight(prop);
            EditorGUI.PropertyField(new Rect(position.position, new(position.width, height)), prop);

            position.y += height + 2;
        }
    }

    public static float CombinePropertyHeights(SerializedProperty property, params string[] relativePropertyPath)
    {
        float height = 2;

        foreach (string propertyPath in relativePropertyPath)
        {
            height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative(propertyPath));
        }

        return height;
    }
}
