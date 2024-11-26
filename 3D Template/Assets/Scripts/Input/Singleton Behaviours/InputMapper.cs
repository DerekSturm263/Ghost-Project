using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class InputMapper : SingletonBehaviour<InputMapper>
{
    [SerializeField] private InputMapSettings _settings;
    public InputMapSettings Settings => _settings;

    private InputDevice _currentDevice;
    public InputDevice CurrentDevice => _currentDevice;

    private IDisposable _event;

    public override void Initialize()
    {
        _currentDevice ??= InputSystem.devices.First(item => _settings.HasControlScheme(item.displayName));
        _event = InputSystem.onAnyButtonPress.Call(SetAllInputDevices);
    }

    public override void Shutdown()
    {
        _event.Dispose();
        _currentDevice = null;
    }

    public void SetAllInputDevices(InputControl action)
    {
        if (!_settings.HasControlScheme(action.device.displayName))
            return;

        _currentDevice = action.device;

        foreach (ButtonPrompt prompt in FindObjectsOfType<ButtonPrompt>())
        {
            prompt.DisplayInput(action.device);
        }
    }
}
