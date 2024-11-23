using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class HelloWorldManager : MonoBehaviour
{
    private NetworkManager _networkManager;

    [SerializeField] private UnityEvent _onNotClientNorServer;
    [SerializeField] private UnityEvent _onElse;

    [SerializeField] private UnityEvent<string> _onEvaluateTransport;
    [SerializeField] private UnityEvent<string> _onEvaluateMode;

    private void Awake()
    {
        _networkManager = GetComponent<NetworkManager>();
    }

    private void Start()
    {
        EvaluateNetwork();
    }

    public void EvaluateNetwork()
    {
        if (!_networkManager.IsClient && !_networkManager.IsServer)
            _onNotClientNorServer.Invoke();
        else
            _onElse.Invoke();
    }

    public void StartHost()
    {
        _networkManager.StartHost();
        EvaluateNetwork();
    }

    public void StartClient()
    {
        _networkManager.StartClient();
        EvaluateNetwork();
    }

    public void StartServer()
    {
        _networkManager.StartServer();
        EvaluateNetwork();
    }

    public void EvaluateTransport()
    {
        _onEvaluateTransport.Invoke($"Transport: {_networkManager.NetworkConfig.NetworkTransport.GetType().Name}");
    }

    public void EvaluateMode()
    {
        string mode = _networkManager.IsHost ? "Host" : _networkManager.IsServer ? "Server" : "Client";
        _onEvaluateMode.Invoke($"Mode: {mode}");
    }

    public void Move()
    {
        foreach (ulong uid in _networkManager.ConnectedClientsIds)
        {
            _networkManager.SpawnManager.GetPlayerNetworkObject(uid).GetComponent<HelloWorldPlayer>().Move();
        }
    }

    public void RequestPositionChange()
    {
        NetworkObject playerObject = _networkManager.SpawnManager.GetLocalPlayerObject();

        if (playerObject.TryGetComponent(out HelloWorldPlayer player))
            player.Move();
    }
}
