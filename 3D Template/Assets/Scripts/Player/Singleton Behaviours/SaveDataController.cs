using UnityEngine;

public class SaveDataController : SingletonBehaviour<SaveDataController>
{
    [SerializeField] private SaveData _default;
    [SerializeField] private string _directory;

    private Data _currentData;
    public Data CurrentData => _currentData;

    public override void Initialize()
    {
        base.Initialize();

        _currentData = SerializationHelper.Load(_default.Data, _directory, "SaveData.json");
    }

    public override void Shutdown()
    {
        SerializationHelper.Save(_currentData, _directory, "SaveData.json");

        base.Shutdown();
    }
}
