using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine.InputSystem;

public class PlayerJoinController : SingletonBehaviour<PlayerJoinController>
{
    private NetworkManager _networkManager;

    private List<LocalPlayer<Controls>> _allPlayers = new();
    private Controls _controls;

    private void Awake()
    {
        _networkManager = FindObjectOfType<NetworkManager>();

        _controls = new();

        _controls.Menu.Join.performed += TryPlayerJoin;
        _controls.Menu.Leave.performed += TryPlayerLeave;
    }

    private void TryPlayerJoin(InputAction.CallbackContext ctx)
    {
        
    }

    private void TryPlayerLeave(InputAction.CallbackContext ctx)
    {

    }
}
