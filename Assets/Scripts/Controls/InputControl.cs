using Controls.Gamepads;
using Heroes;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Controls
{
    [RequireComponent(typeof(GamepadEffects))]
    public class InputControl : MonoBehaviour
    {
        private InputDevices _inputDevices;
        private GamepadEffects _gamepadEffects;

        private PlayerInput _playerInput;

        [Inject]
        public void Construct(PlayerInput playerInput)
        {
            _playerInput = playerInput;
        }

        public void Initialize(Hero hero)
        {
            _gamepadEffects = GetComponent<GamepadEffects>();

            _inputDevices = new InputDevices(_playerInput);
            _gamepadEffects.Initialize(_inputDevices, hero);
        }

        public void Dispose()
        {
            _inputDevices.Dispose();
            _gamepadEffects.Dispose();
        }
    }
}
