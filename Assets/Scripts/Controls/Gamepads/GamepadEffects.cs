﻿using System;
using Heroes;
using UnityEngine;

namespace Controls.Gamepads
{
    [RequireComponent(typeof(GamepadRumbling))]
    [RequireComponent(typeof(InputDevices))]
    public class GamepadEffects : MonoBehaviour
    {
        [SerializeField] private StrokeRumble _strokeRumble = new();

        private Hero _hero;

        private GamepadRumbling _rumbling;

        private InputDevices _inputDevices;

        public void Initialize(Hero hero)
        {
            _hero = hero;

            FillComponents();

            _rumbling.Initialize();

            if (_inputDevices.ActiveDevice == Device.Gamepad)
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
            if (_inputDevices.ActiveDevice == Device.Gamepad)
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
            _rumbling.StartConstant(_strokeRumble.Strength, _strokeRumble.Strength, _strokeRumble.Duration);
        }

        private void FillComponents()
        {
            _rumbling = GetComponent<GamepadRumbling>();
            _inputDevices = GetComponent<InputDevices>();
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
