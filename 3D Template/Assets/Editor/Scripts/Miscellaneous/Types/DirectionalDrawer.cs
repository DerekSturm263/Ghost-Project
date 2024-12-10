using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Directional<>))]
internal class DirectionalDrawer : PropertyDrawer
{
    private int _toolbarIndex;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        Rect toolbarRect = new(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        string[] toolbarTabs = new string[] { "N", "NE", "E", "SE", "S", "SW", "W", "NW" };
        _toolbarIndex = GUI.Toolbar(toolbarRect, _toolbarIndex, toolbarTabs);

        string propertyName = GetPropertyFromToolbar();

        SerializedProperty direction = property.FindPropertyRelative(propertyName);

        float height = EditorGUI.GetPropertyHeight(direction);
        Rect directionalRect = new(position.x, position.y + EditorGUIUtility.singleLineHeight + 2, position.width, height);
        EditorGUI.PropertyField(directionalRect, direction);

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        string propertyName = GetPropertyFromToolbar();
        SerializedProperty direction = property.FindPropertyRelative(propertyName);

        return EditorGUI.GetPropertyHeight(direction) + EditorGUIUtility.singleLineHeight;
    }

    private string GetPropertyFromToolbar() => _toolbarIndex switch
    {
        1 => "_northEast",
        2 => "_east",
        3 => "_southEast",
        4 => "_south",
        5 => "_southWest",
        6 => "_west",
        7 => "_northWest",
        _ => "_north"
    };
}
