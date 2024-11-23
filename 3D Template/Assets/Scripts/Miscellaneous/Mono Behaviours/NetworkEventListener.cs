using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class NetworkEventListener : MonoBehaviour
{
    private NetworkManager _networkManager;

    [SerializeField] private UnityEvent _onHost;
    [SerializeField] private UnityEvent _onClient;
    [SerializeField] private UnityEvent _onServer;

    private void Awake()
    {
        _networkManager = FindObjectOfType<NetworkManager>();
    }

    private void OnEnable()
    {
        if (_networkManager.IsHost)
            _onHost.Invoke();
        else if (_networkManager.IsClient)
            _onClient.Invoke();
        else if (_networkManager.IsServer)
            _onServer.Invoke();
    }
}
