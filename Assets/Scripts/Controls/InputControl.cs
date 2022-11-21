using Controls.Gamepads;
using Heroes;
using Heroes.Services;
using Infrastructure.Controls;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Controls
{
    [RequireComponent(typeof(GamepadEffects))]
    public class InputControl : MonoBehaviour, ITickable
    {
        private InputDevices _inputDevices;
        private GamepadEffects _gamepadEffects;

        private PlayerInput _playerInput;

        [Inject]
        public void Construct(PlayerInput playerInput)
        {
            _playerInput = playerInput;
        }

        public void Initialize(HeroesControl heroesControl)
        {
            _gamepadEffects = GetComponent<GamepadEffects>();

            _inputDevices = new InputDevices(_playerInput);
            _gamepadEffects.Initialize(_inputDevices, heroesControl.First);
        }

        public void Dispose()
        {
            _inputDevices.Dispose();
            _gamepadEffects.Dispose();
        }

        public void Tick()
        {
            _gamepadEffects.Tick();
        }
    }
}
