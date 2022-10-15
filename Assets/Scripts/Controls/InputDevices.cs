using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Controls
{
    public class InputDevices : MonoBehaviour
    {
        private const string KeyboardAndMouseScheme = "KeyaboardAndMouse";
        private const string GamepadScheme = "Gamepad";

        public event Action ActiveDeviceChange;

        public Device ActiveDevice { get; private set; }
        public Gamepad Gamepad { get; private set; }

        private PlayerInput _playerInput;

        [Inject]
        public void Construct(PlayerInput playerInput)
        {
            _playerInput = playerInput;
        }

        public void Initialize()
        {
            _playerInput.onControlsChanged += OnControlsChange;

            FindGamepad();
        }

        public void Dispose()
        {
            _playerInput.onControlsChanged -= OnControlsChange;
        }

        private void OnControlsChange(PlayerInput playerInput)
        {
            ActiveDevice = _playerInput.currentControlScheme switch
            {
                KeyboardAndMouseScheme => Device.KeyboardAndMouse,
                GamepadScheme => Device.Gamepad,
                _ => ActiveDevice
            };

            if (ActiveDevice == Device.Gamepad)
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
