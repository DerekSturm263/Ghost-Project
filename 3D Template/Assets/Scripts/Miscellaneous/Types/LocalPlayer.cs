using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

[Serializable]
public readonly struct LocalPlayer<TControls> where TControls : IInputActionCollection2, IDisposable, new()
{
    [SerializeField] private readonly InputUser _user;
    [SerializeField] private readonly InputDevice _device;
    [SerializeField] private readonly TControls _controls;

    public readonly TControls Controls => _controls;

    public LocalPlayer(InputDevice device)
    {
        if (device is not null)
        {
            _user = InputUser.PerformPairingWithDevice(device);
            _device = device;
            _controls = CreateControls(ref _user, _device);
        }
        else
        {
            _user = InputUser.CreateUserWithoutPairedDevices();
            _device = InputSystem.devices.FirstOrDefault();
            _controls = new();
        }

        Debug.Log($"Player {_user.index + 1} created with device {_device.displayName}");
    }

    public static TControls CreateControls(ref InputUser user, InputDevice device)
    {
        TControls controls = new();
        user.AssociateActionsWithUser(controls);

        InputControlScheme? scheme = InputControlScheme.FindControlSchemeForDevice(device, controls.controlSchemes);
        if (scheme.HasValue)
        {
            user.ActivateControlScheme(scheme.Value);
        }

        return controls;
    }

    public readonly void StartVibrations()
    {
        if (_device is Gamepad gamepad)
            gamepad.ResumeHaptics();
    }

    public readonly void EndVibrations()
    {
        if (_device is Gamepad gamepad)
            gamepad.PauseHaptics();
    }

    public readonly void VibrateOverTime(float frequency)
    {
        if (_device is Gamepad gamepad)
            gamepad.SetMotorSpeeds(frequency, frequency);
    }

    public readonly void VibrateOnce(MonoBehaviour sender, float frequency, float duration)
    {
        if (_device is Gamepad gamepad)
            sender.StartCoroutine(VibrateOnceCoroutine(gamepad, frequency, duration));
    }

    private IEnumerator VibrateOnceCoroutine(Gamepad gamepad, float frequency, float duration)
    {
        gamepad.SetMotorSpeeds(frequency, frequency);

        gamepad.ResumeHaptics();
        yield return new WaitForSeconds(duration);
        gamepad.PauseHaptics();
    }

    public readonly void Enable() => _controls.Enable();
    public readonly void Disable() => _controls.Disable();
}
