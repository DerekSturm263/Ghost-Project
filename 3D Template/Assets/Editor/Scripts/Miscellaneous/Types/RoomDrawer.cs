using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Room))]
internal class RoomDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        SerializedProperty name = property.FindPropertyRelative("_name");
        SerializedProperty layout = property.FindPropertyRelative("_layout");
        SerializedProperty roomsToDirections = property.FindPropertyRelative("_roomsToDirections");

        Rect nameRect = new(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(nameRect, name);

        float height = EditorGUI.GetPropertyHeight(layout);
        Rect layoutRect = new(position.x, position.y + EditorGUIUtility.singleLineHeight + 2, position.width, height);
        EditorGUI.PropertyField(layoutRect, layout);

        Rect directionalRect = new(position.x, position.y + EditorGUIUtility.singleLineHeight + height + 2, position.width, height);
        EditorGUI.PropertyField(directionalRect, roomsToDirections);

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        SerializedProperty name = property.FindPropertyRelative("_name");
        SerializedProperty layout = property.FindPropertyRelative("_layout");
        SerializedProperty roomsToDirections = property.FindPropertyRelative("_roomsToDirections");

        return EditorGUI.GetPropertyHeight(name) + EditorGUI.GetPropertyHeight(layout) + EditorGUI.GetPropertyHeight(roomsToDirections);
    }
}
