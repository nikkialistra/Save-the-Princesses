using System;
using Heroes;
using UnityEngine;

namespace Controls.Gamepads
{
    [RequireComponent(typeof(GamepadRumbling))]
    public class GamepadEffects : MonoBehaviour
    {
        [SerializeField] private StrokeRumble _strokeHitRumble = new();

        private Hero _hero;

        private GamepadRumbling _rumbling;

        private InputDevices _inputDevices;

        public void Initialize(InputDevices inputDevices, Hero hero)
        {
            _inputDevices = inputDevices;
            _hero = hero;

            _rumbling = GetComponent<GamepadRumbling>();

            _rumbling.Initialize(inputDevices);

            if (_inputDevices.ActiveDevice == DeviceType.Gamepad)
                SubscribeToGameEvents();

            _inputDevices.ActiveDeviceChange += OnActiveDeviceChange;
        }

        public void Dispose()
        {
            _rumbling.Dispose();

            UnsubscribeFromGameEvents();

            _inputDevices.ActiveDeviceChange -= OnActiveDeviceChange;
        }

        private void OnActiveDeviceChange()
        {
            if (_inputDevices.ActiveDevice == DeviceType.Gamepad)
                SubscribeToGameEvents();
            else
                UnsubscribeFromGameEvents();
        }

        private void SubscribeToGameEvents()
        {
            _hero.StrokeStart += OnStrokeStart;
        }

        private void UnsubscribeFromGameEvents()
        {
            _hero.StrokeStart -= OnStrokeStart;
        }

        private void OnStrokeStart()
        {
            _rumbling.StartConstant(_strokeHitRumble.Strength, _strokeHitRumble.Strength, _strokeHitRumble.Duration);
        }

        [Serializable]
        private class StrokeRumble
        {
            [Range(0, 1f)]
            public float Strength;
            [Range(0, 3f)]
            public float Duration;
        }
    }
}
