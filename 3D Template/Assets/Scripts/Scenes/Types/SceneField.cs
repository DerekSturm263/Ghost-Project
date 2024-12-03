using System.Linq;
using UnityEngine;

[System.Serializable]
public class SceneField
{
#if UNITY_EDITOR

    [SerializeField] private Object _SceneAsset;

#endif
    [SerializeField] private string _SceneName = "";

    public string Name
    {
        get => _SceneName;
        set => SetByName(value);
    }

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

        _SceneAsset = UnityEditor.AssetDatabase.LoadAssetAtPath<UnityEditor.SceneAsset>(scenePath);

#endif
        _SceneName = sceneName;
    }
}
