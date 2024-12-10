using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(CastHelper.BoxCastSettings))]
internal class BoxCastSettingsDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        EditorGUI.PrefixLabel(position, label, EditorStyles.boldLabel);
        position.y += EditorGUIUtility.singleLineHeight;

        EditorHelper.DrawProperties(position, property, "_offset", "_size", "_rotation");

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => EditorHelper.CombinePropertyHeights(property, "_offset", "_size", "_rotation") + EditorGUIUtility.singleLineHeight;
}

[CustomPropertyDrawer(typeof(CastHelper.SphereCastSettings))]
internal class SphereCastSettingsDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        EditorGUI.PrefixLabel(position, label, EditorStyles.boldLabel);
        position.y += EditorGUIUtility.singleLineHeight;

        EditorHelper.DrawProperties(position, property, "_offset", "_radius");

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => EditorHelper.CombinePropertyHeights(property, "_offset", "_radius") + EditorGUIUtility.singleLineHeight;
}

[CustomPropertyDrawer(typeof(CastHelper.CapsuleCastSettings))]
internal class CapsuleCastSettingsDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        EditorGUI.PrefixLabel(position, label, EditorStyles.boldLabel);
        position.y += EditorGUIUtility.singleLineHeight;

        EditorHelper.DrawProperties(position, property, "_points", "_radius");

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => EditorHelper.CombinePropertyHeights(property, "_points", "_radius") + EditorGUIUtility.singleLineHeight;
}

[CustomPropertyDrawer(typeof(CastHelper.RayCastSettings))]
internal class RayCastSettingsDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        EditorGUI.PrefixLabel(position, label, EditorStyles.boldLabel);
        position.y += EditorGUIUtility.singleLineHeight;

        EditorHelper.DrawProperties(position, property);

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => EditorHelper.CombinePropertyHeights(property) + EditorGUIUtility.singleLineHeight;
}
