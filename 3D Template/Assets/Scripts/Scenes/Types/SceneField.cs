using System.Linq;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class SceneField
{
#if UNITY_EDITOR
#pragma warning disable 414 // Supresses "The private field `SceneField.m_SceneAsset' is assigned but its value is never used." Is used by the PropertyDrawer using string reference
    [SerializeField]
    private Object m_SceneAsset;
#pragma warning restore 414
#endif
    [SerializeField]
    private string m_SceneName = "";
    public string Name
    {
        get { return m_SceneName; }
        set { SetByName(value); }
    }

    // makes it work with the existing Unity methods (LoadLevel/LoadScene)
    public static implicit operator string(SceneField sceneField)
    {
        return sceneField == null ? "" : sceneField.Name;
    }

    private void SetByName(string sceneName)
    {
#if UNITY_EDITOR
        var scenePath = UnityEditor.EditorBuildSettings.scenes
            .Select(s => s.path)
            .FirstOrDefault(s => s.EndsWith(sceneName + ".unity"));
        m_SceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);
#endif
        m_SceneName = sceneName;
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(SceneField))]
public class SceneFieldPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label)
    {
        EditorGUI.BeginProperty(_position, GUIContent.none, _property);
        SerializedProperty sceneAsset = _property.FindPropertyRelative("m_SceneAsset");
        SerializedProperty sceneName = _property.FindPropertyRelative("m_SceneName");
        _position = EditorGUI.PrefixLabel(_position, GUIUtility.GetControlID(FocusType.Passive), _label);
        if (sceneAsset != null)
        {
            EditorGUI.BeginChangeCheck();
            Object value = EditorGUI.ObjectField(_position, sceneAsset.objectReferenceValue, typeof(SceneAsset), false);
            if (EditorGUI.EndChangeCheck())
            {
                sceneAsset.objectReferenceValue = value;
                if (sceneAsset.objectReferenceValue != null)
                {
                    sceneName.stringValue = (sceneAsset.objectReferenceValue as SceneAsset).name;
                }
                else
                {
                    sceneName.stringValue = "";
                }
            }
        }
        EditorGUI.EndProperty();
    }
}
#endif
