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

        switch (index.intValue)
        {
            case 0:
                float height1 = EditorGUI.GetPropertyHeight(value1);
                Rect value1Rect = new(position.x, position.y + EditorGUIUtility.singleLineHeight + 2, position.width, height1);
                EditorGUI.PropertyField(value1Rect, value1);

                break;

            case 1:
                float height2 = EditorGUI.GetPropertyHeight(value2);
                Rect value2Rect = new(position.x, position.y + EditorGUIUtility.singleLineHeight + 2, position.width, height2);
                EditorGUI.PropertyField(value2Rect, value2);

                break;
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        SerializedProperty index = property.FindPropertyRelative("_index");

        return index.intValue switch
        {
            0 => EditorGUI.GetPropertyHeight(property.FindPropertyRelative("_value1")) + EditorGUIUtility.singleLineHeight,
            1 => EditorGUI.GetPropertyHeight(property.FindPropertyRelative("_value2")) + EditorGUIUtility.singleLineHeight,
            _ => default
        };
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

        switch (index.intValue)
        {
            case 0:
                float height1 = EditorGUI.GetPropertyHeight(value1);
                Rect value1Rect = new(position.x, position.y + EditorGUIUtility.singleLineHeight + 2, position.width, height1);
                EditorGUI.PropertyField(value1Rect, value1);

                break;

            case 1:
                float height2 = EditorGUI.GetPropertyHeight(value2);
                Rect value2Rect = new(position.x, position.y + EditorGUIUtility.singleLineHeight + 2, position.width, height2);
                EditorGUI.PropertyField(value2Rect, value2);

                break;

            case 2:
                float height3 = EditorGUI.GetPropertyHeight(value3);
                Rect value3Rect = new(position.x, position.y + EditorGUIUtility.singleLineHeight + 2, position.width, height3);
                EditorGUI.PropertyField(value3Rect, value3);

                break;
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        SerializedProperty index = property.FindPropertyRelative("_index");

        return index.intValue switch
        {
            0 => EditorGUI.GetPropertyHeight(property.FindPropertyRelative("_value1")) + EditorGUIUtility.singleLineHeight,
            1 => EditorGUI.GetPropertyHeight(property.FindPropertyRelative("_value2")) + EditorGUIUtility.singleLineHeight,
            2 => EditorGUI.GetPropertyHeight(property.FindPropertyRelative("_value3")) + EditorGUIUtility.singleLineHeight,
            _ => default
        };
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

        switch (index.intValue)
        {
            case 0:
                float height1 = EditorGUI.GetPropertyHeight(value1);
                Rect value1Rect = new(position.x, position.y + EditorGUIUtility.singleLineHeight + 2, position.width, height1);
                EditorGUI.PropertyField(value1Rect, value1);

                break;

            case 1:
                float height2 = EditorGUI.GetPropertyHeight(value2);
                Rect value2Rect = new(position.x, position.y + EditorGUIUtility.singleLineHeight + 2, position.width, height2);
                EditorGUI.PropertyField(value2Rect, value2);

                break;

            case 2:
                float height3 = EditorGUI.GetPropertyHeight(value3);
                Rect value3Rect = new(position.x, position.y + EditorGUIUtility.singleLineHeight + 2, position.width, height3);
                EditorGUI.PropertyField(value3Rect, value3);

                break;

            case 3:
                float height4 = EditorGUI.GetPropertyHeight(value4);
                Rect value4Rect = new(position.x, position.y + EditorGUIUtility.singleLineHeight + 2, position.width, height4);
                EditorGUI.PropertyField(value4Rect, value4);

                break;
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        SerializedProperty index = property.FindPropertyRelative("_index");

        return index.intValue switch
        {
            0 => EditorGUI.GetPropertyHeight(property.FindPropertyRelative("_value1")) + EditorGUIUtility.singleLineHeight,
            1 => EditorGUI.GetPropertyHeight(property.FindPropertyRelative("_value2")) + EditorGUIUtility.singleLineHeight,
            2 => EditorGUI.GetPropertyHeight(property.FindPropertyRelative("_value3")) + EditorGUIUtility.singleLineHeight,
            3 => EditorGUI.GetPropertyHeight(property.FindPropertyRelative("_value4")) + EditorGUIUtility.singleLineHeight,
            _ => default
        };
    }
}
