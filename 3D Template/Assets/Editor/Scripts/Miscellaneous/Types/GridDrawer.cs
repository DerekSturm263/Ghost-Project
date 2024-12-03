using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Grid<>))]
internal class GridDrawer : PropertyDrawer
{
    private bool _isFoldedOut;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty width = property.FindPropertyRelative("_width");
        SerializedProperty height = property.FindPropertyRelative("_height");
        SerializedProperty elements = property.FindPropertyRelative("_elements");

        Rect foldoutRect = new(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        _isFoldedOut = EditorGUI.Foldout(foldoutRect, _isFoldedOut, label);

        int oldWidth = width.intValue;
        Rect widthRect = new(position.x + position.width - 93, position.y, 50, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(widthRect, width, new GUIContent(""));

        int oldHeight = height.intValue;
        Rect heightRect = new(position.x + position.width - 50, position.y, 50, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(heightRect, height, new GUIContent(""));

        if (width.intValue != oldWidth || height.intValue != oldHeight)
        {
            while (elements.arraySize != width.intValue * height.intValue)
            {
                if (elements.arraySize > width.intValue * height.intValue)
                {
                    elements.DeleteArrayElementAtIndex(0);
                }
                else
                {
                    elements.InsertArrayElementAtIndex(0);
                }
            }
        }

        if (!_isFoldedOut)
            return;

        for (int y = 0; y < height.intValue; ++y)
        {
            for (int x = 0; x < width.intValue; ++x)
            {
                Rect elementRect = new(position.x + x * 20, position.y + EditorGUIUtility.singleLineHeight + y * 20, 20, 20);
                EditorGUI.PropertyField(elementRect, elements.GetArrayElementAtIndex(y * width.intValue + x), new GUIContent(""));
            }
        }

        Rect buttonPosEast = new(position.x + width.intValue * 20 + 10, position.y + 20, 15, height.intValue * 10);
        if (GUI.Button(buttonPosEast, "+"))
        {
            ++width.intValue;

            for (int i = 0; i < height.intValue; ++i)
                elements.InsertArrayElementAtIndex(0);
        }

        Rect buttonPosEast2 = new(position.x + width.intValue * 20 + 10, position.y + 20 + height.intValue * 10, 15, height.intValue * 10);
        if (GUI.Button(buttonPosEast2, "-"))
        {
            --width.intValue;

            for (int i = 0; i < height.intValue; ++i)
                elements.DeleteArrayElementAtIndex(0);
        }

        Rect buttonPosSouth = new(position.x + 10, position.y + (height.intValue + 1) * 20, width.intValue * 10, 15);
        if (GUI.Button(buttonPosSouth, "+"))
        {
            ++height.intValue;

            for (int i = 0; i < width.intValue; ++i)
                elements.InsertArrayElementAtIndex(0);
        }

        Rect buttonPosSouth2 = new(position.x + 10 + width.intValue * 10, position.y + (height.intValue + 1) * 20, width.intValue * 10, 15);
        if (GUI.Button(buttonPosSouth2, "-"))
        {
            --height.intValue;

            for (int i = 0; i < width.intValue; ++i)
                elements.DeleteArrayElementAtIndex(0);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (_isFoldedOut)
        {
            int height = property.FindPropertyRelative("_height").intValue;
            return (height + 2) * 20;
        }
        else
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}
