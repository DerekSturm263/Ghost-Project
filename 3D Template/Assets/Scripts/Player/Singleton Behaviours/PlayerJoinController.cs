using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerJoinController : SingletonBehaviour<PlayerJoinController>
{
    private List<LocalPlayer<Controls>> _localPlayers = new();
    private Controls _controls;

    [SerializeField] private UnityEvent<LocalPlayer<Controls>> _onJoin;
    [SerializeField] private UnityEvent<LocalPlayer<Controls>> _onLeave;

    [SerializeField] private int _playerLimit;
    public int PlayerLimit => _playerLimit;
    public void SetPlayerLimit(int playerLimit) => Instance._playerLimit = playerLimit;

    public IEnumerable<LocalPlayer<Controls>> GetAllLocalPlayers(bool limitCount)
    {
        for (int i = 0; i < _localPlayers.Count; ++i)
        {
            if (limitCount && i == _playerLimit)
                break;

            yield return _localPlayers[i];
        }
    }

    public int GetPlayerCount(bool limitCount) => GetAllLocalPlayers(limitCount).Count();

    public override void Initialize()
    {
        _controls = new();

        _controls.Menu.Join.performed += TryPlayerJoin;
        _controls.Menu.Leave.performed += TryPlayerLeave;
    }

    private void TryPlayerJoin(InputAction.CallbackContext ctx)
    {
        LocalPlayer<Controls> player = new();
        _onJoin.Invoke(player);
    }

    private void TryPlayerLeave(InputAction.CallbackContext ctx)
    {

    }
}
