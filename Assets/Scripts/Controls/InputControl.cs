using Controls.Gamepads;
using Heroes;
using UnityEngine;

namespace Controls
{
    [RequireComponent(typeof(InputDevices))]
    [RequireComponent(typeof(GamepadEffects))]
    public class InputControl : MonoBehaviour
    {
        private InputDevices _inputDevices;
        private GamepadEffects _gamepadEffects;

        public void Initialize(Hero hero)
        {
            _inputDevices = GetComponent<InputDevices>();
            _gamepadEffects = GetComponent<GamepadEffects>();

            _inputDevices.Initialize();
            _gamepadEffects.Initialize(hero);
        }

        public void Dispose()
        {
            _inputDevices.Dispose();
            _gamepadEffects.Dispose();
        }
    }
}
