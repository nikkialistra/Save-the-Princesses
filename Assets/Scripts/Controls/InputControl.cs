using Controls.Gamepads;
using Heroes;
using Heroes.Attacks;
using UnityEngine;

namespace Controls
{
    [RequireComponent(typeof(InputDevices))]
    [RequireComponent(typeof(GamepadEffects))]
    public class InputControl : MonoBehaviour
    {
        private InputDevices _inputDevices;
        private GamepadEffects _gamepadEffects;

        public void Initialize(HeroAttacker heroAttacker)
        {
            _inputDevices = GetComponent<InputDevices>();
            _gamepadEffects = GetComponent<GamepadEffects>();

            _inputDevices.Initialize();
            _gamepadEffects.Initialize(heroAttacker);
        }

        public void Dispose()
        {
            _inputDevices.Dispose();
            _gamepadEffects.Dispose();
        }
    }
}
