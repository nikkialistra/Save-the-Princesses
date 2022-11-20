using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Controls
{
    public class InputDevices
    {
        private const string KeyboardAndMouseScheme = "KeyboardAndMouse";
        private const string GamepadScheme = "Gamepad";

        public event Action ActiveDeviceChange;

        public static DeviceType ActiveDevice { get; private set; }

        public Gamepad Gamepad { get; private set; }

        private readonly PlayerInput _playerInput;

        public InputDevices(PlayerInput playerInput)
        {
            _playerInput = playerInput;

            _playerInput.onControlsChanged += OnControlsChange;

            OnControlsChange(_playerInput);
        }

        public void Dispose()
        {
            _playerInput.onControlsChanged -= OnControlsChange;
        }

        private void OnControlsChange(PlayerInput playerInput)
        {
            ActiveDevice = _playerInput.currentControlScheme switch
            {
                KeyboardAndMouseScheme => DeviceType.KeyboardAndMouse,
                GamepadScheme => DeviceType.Gamepad,
                _ => ActiveDevice
            };

            if (ActiveDevice == DeviceType.Gamepad)
                FindGamepad();

            ActiveDeviceChange?.Invoke();
        }

        private void FindGamepad()
        {
            Gamepad = Gamepad.all.FirstOrDefault(gamepad =>
                _playerInput.devices.Any(inputDevice => inputDevice.deviceId == gamepad.deviceId));
        }
    }
}
