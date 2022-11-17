using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controls.Gamepads
{
    public class GamepadRumbling : MonoBehaviour
    {
        private enum RumblePattern
        {
            Constant,
            Pulse,
            Linear,
            None
        }

        private RumblePattern _activeRumblePattern = RumblePattern.None;

        private float _lowFrequency;
        private float _highFrequency;

        private bool _isMotorActive;

        private float _rumbleStep;

        private float _lowStep;
        private float _highStep;

        private float _rumbleFinishTime;

        private float _nextPulseTime;

        private InputDevices _inputDevices;

        public void Initialize(InputDevices inputDevices)
        {
            _inputDevices = inputDevices;
        }

        public void Dispose()
        {
            Stop();
        }

        public void Tick()
        {
            if (_activeRumblePattern != RumblePattern.None
                && Time.time > _rumbleFinishTime)
            {
                Stop();
                return;
            }

            if (_inputDevices.Gamepad == null)
                return;

            UpdateRumbling();
        }

        [Button]
        public void StartConstant(float lowFrequency, float highFrequency, float duration)
        {
            _activeRumblePattern = RumblePattern.Constant;

            _lowFrequency = lowFrequency;
            _highFrequency = highFrequency;

            _rumbleFinishTime = Time.time + duration;
        }

        [Button]
        public void StartPulse(float lowFrequency, float highFrequency, float burstTime, float duration)
        {
            _activeRumblePattern = RumblePattern.Pulse;

            _lowFrequency = lowFrequency;
            _highFrequency = highFrequency;

            _rumbleStep = burstTime;

            _isMotorActive = false;
            _nextPulseTime = Time.time;

            _rumbleFinishTime = Time.time + duration;
        }

        [Button]
        public void StartLinear(float lowFrequencyStart, float highFrequencyStart, float lowFrequencyEnd,
            float highFrequencyEnd, float duration)
        {
            _activeRumblePattern = RumblePattern.Linear;

            _lowFrequency = lowFrequencyStart;
            _highFrequency = highFrequencyStart;

            _lowStep = (lowFrequencyEnd - lowFrequencyStart) / duration;
            _highStep = (highFrequencyEnd - highFrequencyStart) / duration;

            _rumbleFinishTime = Time.time + duration;
        }

        [Button]
        public void Stop()
        {
            _inputDevices.Gamepad?.ResetHaptics();
            _activeRumblePattern = RumblePattern.None;
        }

        private void UpdateRumbling()
        {
            var gamepad = _inputDevices.Gamepad;

            switch (_activeRumblePattern)
            {
                case RumblePattern.Constant:
                    UpdateConstant(gamepad);
                    break;
                case RumblePattern.Pulse:
                    UpdatePulse(gamepad);
                    break;
                case RumblePattern.Linear:
                    UpdateLinear(gamepad);
                    break;
                case RumblePattern.None:
                    break;
            }
        }

        private void UpdateConstant(Gamepad gamepad)
        {
            gamepad.SetMotorSpeeds(_lowFrequency, _highFrequency);
        }

        private void UpdatePulse(Gamepad gamepad)
        {
            if (Time.time <= _nextPulseTime)
                return;

            _isMotorActive = !_isMotorActive;
            _nextPulseTime = Time.time + _rumbleStep;

            if (_isMotorActive)
                gamepad.SetMotorSpeeds(_lowFrequency, _highFrequency);
            else
                gamepad.SetMotorSpeeds(0, 0);
        }

        private void UpdateLinear(Gamepad gamepad)
        {
            gamepad.SetMotorSpeeds(_lowFrequency, _highFrequency);

            _lowFrequency += _lowStep * Time.deltaTime;
            _highFrequency += _highStep * Time.deltaTime;
        }
    }
}
