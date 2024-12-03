using UnityEngine;

[CreateAssetMenu(fileName = "New Save Data", menuName = "Custom/Save Data")]
public class SaveData : ScriptableObject
{
    [SerializeField] private Data _data;
    public Data Data => _data;
}
