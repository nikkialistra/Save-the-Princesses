using Infrastructure.Installers.Game.Settings;
using UnityEngine;
using Zenject;

namespace Characters
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterMovement : MonoBehaviour
    {
        public Vector2 TargetVelocity { get; private set; }
        public bool Stopped => _rigidBody2d.velocity.magnitude <= _settings.VelocityDelta;

        private float _currentSpeed;
        private Vector2 _direction;

        private Rigidbody2D _rigidBody2d;

        private CharacterSettings _settings;

        [Inject]
        public void Construct(CharacterSettings settings)
        {
            _settings = settings;
        }

        public void Initialize()
        {
            _rigidBody2d = GetComponent<Rigidbody2D>();
        }

        public void UpdateVelocity(float accelerationAmount, float decelerationAmount, float movementSpeed)
        {
            var velocityChange = CalculateVelocityChange(accelerationAmount, decelerationAmount, movementSpeed);

            _currentSpeed += velocityChange;

            _currentSpeed = Mathf.Clamp(_currentSpeed, 0, movementSpeed);

            _rigidBody2d.velocity = _direction * _currentSpeed;
        }

        public void MoveWithSpeed(Vector2 direction, float speed)
        {
            _direction = direction;
            TargetVelocity = direction * speed;
        }

        public void Stop()
        {
            TargetVelocity = Vector2.zero;
        }

        private float CalculateVelocityChange(float accelerationAmount, float decelerationAmount, float movementSpeed)
        {
            var shouldAccelerate = TargetVelocity != Vector2.zero;

            var speedChangeRate = shouldAccelerate ? accelerationAmount : decelerationAmount;
            var targetSpeed = shouldAccelerate ? movementSpeed : 0;

            var differenceFromTarget = targetSpeed - _currentSpeed;

            return speedChangeRate * differenceFromTarget;
        }
    }
}
