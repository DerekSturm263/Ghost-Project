using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapSettings))]
[CanEditMultipleObjects]
public class MapSettingsEditor : Editor
{
    private int _toolbarIndex;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MapSettings mapSettings = target as MapSettings;

        serializedObject.Update();

        List<string> items = new();
        items = mapSettings.Rooms.Select(item => item.Name).ToList();
        items.Add("+");

        EditorGUILayout.Space(10);
        _toolbarIndex = GUILayout.Toolbar(_toolbarIndex, items.ToArray());

        EditorGUILayout.PropertyField(serializedObject.FindProperty("_rooms").GetArrayElementAtIndex(_toolbarIndex));

        serializedObject.ApplyModifiedProperties();
    }
}
