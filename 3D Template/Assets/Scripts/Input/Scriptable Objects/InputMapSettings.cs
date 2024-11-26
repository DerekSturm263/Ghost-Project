using UnityEngine;

[CreateAssetMenu(fileName = "New Input Map Settings", menuName = "Custom/Input Map Settings")]
public class InputMapSettings : ScriptableObject
{
    [SerializeField] private Dictionary<string, Tuple<TMPro.TMP_SpriteAsset, Sprite>> _controlSchemesToPrompts;

    public bool HasControlScheme(string controlScheme) => _controlSchemesToPrompts.ContainsKey(controlScheme);
    public TMPro.TMP_SpriteAsset GetSpriteAsset(string controlScheme) => _controlSchemesToPrompts[controlScheme].Item1;
    public Sprite GetSprite(string controlScheme) => _controlSchemesToPrompts[controlScheme].Item2;
}
