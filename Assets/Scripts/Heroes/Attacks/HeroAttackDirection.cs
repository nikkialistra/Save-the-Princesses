using Controls;
using GameData.Settings;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using DeviceType = Controls.DeviceType;

namespace Heroes.Attacks
{
    public class HeroAttackDirection : MonoBehaviour
    {
        private float AngleFromDirection => Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        private float Direction => transform.rotation.eulerAngles.z;

        private Vector2 _direction = new(0, -1);

        private InputDevices _inputDevices;
        private Camera _camera;

        private Hero _hero;

        private InputAction _lookAction;

        [Inject]
        public void Construct(Hero hero, InputDevices inputDevices, Camera camera, PlayerInput playerInput)
        {
            _hero = hero;

            _inputDevices = inputDevices;
            _camera = camera;

            _lookAction = playerInput.actions.FindAction("Look");
        }

        public void Tick()
        {
            UpdateAttackDirection();
            UpdateTransform();
            UpdateAttackRotation();
        }

        private void UpdateAttackDirection()
        {
            switch (_inputDevices.ActiveDevice)
            {
                case DeviceType.Gamepad:
                    ComputeFromGamepad();
                    break;
                case DeviceType.KeyboardAndMouse:
                    ComputeFromMouse();
                    break;
            }
        }

        private void UpdateTransform()
        {
            if (_direction == Vector2.zero || Time.timeScale == 0) return;

            transform.localPosition = _hero.PositionCenterOffset + (_direction.normalized * GameSettings.Hero.AttackDirectionDistance);
            transform.rotation = Quaternion.Euler(0, 0, AngleFromDirection);
        }

        private void UpdateAttackRotation()
        {
            _hero.UpdateAttackRotation(Direction);
        }

        private void ComputeFromGamepad()
        {
            var lookDirection = _lookAction.ReadValue<Vector2>();

            if (lookDirection != Vector2.zero)
                _direction = lookDirection;
        }

        private void ComputeFromMouse()
        {
            var lookPosition = _lookAction.ReadValue<Vector2>();

            var mousePosition = (Vector2)_camera.ScreenToWorldPoint(lookPosition);

            _direction = mousePosition - _hero.PositionCenter;
        }
    }
}
