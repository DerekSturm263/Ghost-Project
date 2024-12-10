using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Variant<,>))]
internal class Variant1Drawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty index = property.FindPropertyRelative("_index");
        SerializedProperty value1 = property.FindPropertyRelative("_value1");
        SerializedProperty value2 = property.FindPropertyRelative("_value2");

        Rect toolbarRect = new(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        string[] toolbarTabs = new string[] { value1.type, value2.type };
        index.intValue = GUI.Toolbar(toolbarRect, index.intValue, toolbarTabs);

        SerializedProperty childProperty = index.intValue switch
        {
            1 => value2,
            _ => value1
        };

        float height = EditorGUI.GetPropertyHeight(childProperty);
        Rect valueRect = new(position.x, position.y + EditorGUIUtility.singleLineHeight + 2, position.width, height);
        EditorGUI.PropertyField(valueRect, childProperty, new GUIContent("Value"));
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        SerializedProperty index = property.FindPropertyRelative("_index");

        return EditorGUI.GetPropertyHeight(property.FindPropertyRelative(index.intValue switch
        {
            1 => "_value2",
            _ => "_value1"
        })) + EditorGUIUtility.singleLineHeight;
    }
}
[CustomPropertyDrawer(typeof(Variant<,,>))]
internal class Variant2Drawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty index = property.FindPropertyRelative("_index");
        SerializedProperty value1 = property.FindPropertyRelative("_value1");
        SerializedProperty value2 = property.FindPropertyRelative("_value2");
        SerializedProperty value3 = property.FindPropertyRelative("_value3");

        Rect toolbarRect = new(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        string[] toolbarTabs = new string[] { value1.type, value2.type, value3.type };
        index.intValue = GUI.Toolbar(toolbarRect, index.intValue, toolbarTabs);

        SerializedProperty childProperty = index.intValue switch
        {
            1 => value2,
            2 => value3,
            _ => value1
        };

        float height = EditorGUI.GetPropertyHeight(childProperty);
        Rect valueRect = new(position.x, position.y + EditorGUIUtility.singleLineHeight + 2, position.width, height);
        EditorGUI.PropertyField(valueRect, childProperty, new GUIContent("Value"));
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        SerializedProperty index = property.FindPropertyRelative("_index");

        return EditorGUI.GetPropertyHeight(property.FindPropertyRelative(index.intValue switch
        {
            1 => "_value2",
            2 => "_value3",
            _ => "_value1"
        })) + EditorGUIUtility.singleLineHeight;
    }
}

[CustomPropertyDrawer(typeof(Variant<,,,>))]
internal class Variant3Drawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty index = property.FindPropertyRelative("_index");
        SerializedProperty value1 = property.FindPropertyRelative("_value1");
        SerializedProperty value2 = property.FindPropertyRelative("_value2");
        SerializedProperty value3 = property.FindPropertyRelative("_value3");
        SerializedProperty value4 = property.FindPropertyRelative("_value4");

        Rect toolbarRect = new(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        string[] toolbarTabs = new string[] { value1.type, value2.type, value3.type, value4.type };
        index.intValue = GUI.Toolbar(toolbarRect, index.intValue, toolbarTabs);

        SerializedProperty childProperty = index.intValue switch
        {
            1 => value2,
            2 => value3,
            3 => value4,
            _ => value1
        };

        float height = EditorGUI.GetPropertyHeight(childProperty);
        Rect valueRect = new(position.x, position.y + EditorGUIUtility.singleLineHeight + 2, position.width, height);
        EditorGUI.PropertyField(valueRect, childProperty, new GUIContent("Value"));
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        SerializedProperty index = property.FindPropertyRelative("_index");

        return EditorGUI.GetPropertyHeight(property.FindPropertyRelative(index.intValue switch
        {
            1 => "_value2",
            2 => "_value3",
            3 => "_value4",
            _ => "_value1"
        })) + EditorGUIUtility.singleLineHeight;
    }
}
