using GameData.Settings;
using UnityEngine;

namespace Characters.Moving.Elements
{
    public class CharacterMovement
    {
        public Vector2 TargetVelocity { get; private set; }
        public bool Stopped => _rigidBody2D.velocity.magnitude <= GameSettings.Character.VelocityDelta;

        private float _currentSpeed;
        private Vector2 _direction;

        private readonly Rigidbody2D _rigidBody2D;

        public CharacterMovement(Rigidbody2D rigidbody2D)
        {
            _rigidBody2D = rigidbody2D;
        }

        public void UpdateVelocity(float accelerationAmount, float decelerationAmount, float movementSpeed)
        {
            var velocityChange = CalculateVelocityChange(accelerationAmount, decelerationAmount, movementSpeed);

            _currentSpeed += velocityChange;

            _currentSpeed = Mathf.Clamp(_currentSpeed, 0, movementSpeed);

            _rigidBody2D.velocity = _direction * _currentSpeed;
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
