using Infrastructure.CompositionRoot.Settings;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Heroes
{
    public class HeroInput : MonoBehaviour
    {
        public Vector2 MoveInput => _moveInput;
        public Vector2 DirectionNormalized => _directionNormalized;

        private bool BothAxesWereUsed => _previousMoveInput.x != 0 && _previousMoveInput.y != 0;
        private bool BothAxesWereChanged => Mathf.Sign(_moveInput.x) != Mathf.Sign(_previousMoveInput.x)
                                            && Mathf.Sign(_moveInput.y) != Mathf.Sign(_previousMoveInput.y);

        private Vector2 _moveInput;
        private Vector2 _previousMoveInput;

        private Vector2 _directionNormalized;

        private float _lastPressX = float.NegativeInfinity;
        private float _lastPressY = float.NegativeInfinity;

        private HeroSettings _settings;

        private PlayerInput _playerInput;

        private InputAction _moveAction;

        [Inject]
        public void Construct(PlayerInput playerInput, HeroSettings settings)
        {
            _playerInput = playerInput;

            _settings = settings;
        }

        public void Initialize()
        {
            _moveAction = _playerInput.actions.FindAction("Move");
        }

        private void Update()
        {
            _moveInput = _moveAction.ReadValue<Vector2>();
            SavePressTimes();

            UpdateMoveInputWithButtonTolerance();
            _previousMoveInput = _moveInput;

            if (_moveInput != Vector2.zero)
                _directionNormalized = _moveInput.normalized;
        }

        private void SavePressTimes()
        {
            if (_moveInput.x != 0)
                _lastPressX = Time.time;

            if (_moveInput.y != 0)
                _lastPressY = Time.time;
        }

        private void UpdateMoveInputWithButtonTolerance()
        {
            if (_moveInput == Vector2.zero || BothAxesWereUsed == false || BothAxesWereChanged)
                return;

            if (InputXWasReleased() && TimeBetweenYAndXInputIsSmall())
                _moveInput.x = _previousMoveInput.x;

            if (InputYWasReleased() && TimeBetweenXAndYInputIsSmall())
                _moveInput.y = _previousMoveInput.y;

            bool InputXWasReleased() => _previousMoveInput.x != 0 && _moveInput.x == 0;
            bool InputYWasReleased() => _previousMoveInput.y != 0 && _moveInput.y == 0;

            bool TimeBetweenYAndXInputIsSmall() => _lastPressY - _lastPressX < _settings.ButtonPressToleranceTime;
            bool TimeBetweenXAndYInputIsSmall() => _lastPressX - _lastPressY < _settings.ButtonPressToleranceTime;
        }
    }
}
