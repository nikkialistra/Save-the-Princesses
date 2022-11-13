using DG.Tweening;
using Infrastructure.Installers.Game.Settings;
using UnityEngine;

namespace Characters.Moving.Elements
{
    public class CharacterMoveCalculation
    {
        public Vector2 TargetVelocity => _movement.TargetVelocity;

        public bool Stopped => _movement.Stopped;

        public float MovementSpeed => _moving.MovementSpeed;

        private float AccelerationAmount => (MovementSpeed / _settings.AccelerationTime) * Time.fixedDeltaTime;
        private float DecelerationAmount => (MovementSpeed / _settings.DecelerationTime) * Time.fixedDeltaTime;

        private bool Blocked => _atKnockback || _stunned;

        private bool _atKnockback;
        private bool _stunned;

        private readonly CharacterMoving _moving;
        private readonly CharacterMovement _movement;
        private readonly CharacterPathfinding _pathfinding;
        private readonly Rigidbody2D _rigidBody2d;
        private readonly CharacterSettings _settings;

        public CharacterMoveCalculation(CharacterMoving moving, CharacterMovement movement,
            CharacterPathfinding pathfinding, Rigidbody2D rigidbody2D, CharacterSettings settings)
        {
            _moving = moving;
            _movement = movement;
            _pathfinding = pathfinding;
            _rigidBody2d = rigidbody2D;
            _settings = settings;

            SubscribeToEvents();
        }

        public void Dispose()
        {
            UnsubscribeFromEvents();
        }

        public void FixedTick()
        {
            _movement.UpdateVelocity(AccelerationAmount, DecelerationAmount, MovementSpeed);
        }

        public void Move(Vector2 direction)
        {
            if (Blocked) return;

            MoveWithSpeed(direction, MovementSpeed);
        }

        public void FindPathTo(Vector2 destination)
        {
            _pathfinding.SetDestination(destination);
        }

        public void Stop()
        {
            _movement.Stop();
        }

        public void ResetMove()
        {
            Stop();
            _pathfinding.ResetDestination();
        }

        public void Knockback(Vector2 value)
        {
            Stop();
            _atKnockback = true;

            var targetPosition = _rigidBody2d.position + value;

            DOTween.To(() => _rigidBody2d.position, x => _rigidBody2d.position = x, targetPosition,
                    _settings.TimeAtKnockback)
                .SetEase(Ease.OutQuint)
                .OnComplete(ResetKnockback);
        }

        private void MoveWith(Vector2 velocity)
        {
            if (velocity.magnitude <= _settings.VelocityDelta)
            {
                Stop();
                return;
            }

            var direction = velocity.normalized;
            Move(direction);
        }

        private void MoveTo(Vector2 destination)
        {
            var delta = destination - (Vector2)_moving.transform.position;

            if (delta.magnitude <= _settings.DestinationDistanceDelta)
            {
                Stop();
                return;
            }

            var direction = delta.normalized;
            Move(direction);
        }

        private void MoveWithSpeed(Vector2 direction, float speed)
        {
            _movement.MoveWithSpeed(direction, speed);
        }

        private void ResetKnockback()
        {
            _atKnockback = false;
        }

        private void OnAtStun()
        {
            _stunned = true;
        }

        private void OnAtStunEnd()
        {
            _stunned = false;
        }

        private void SubscribeToEvents()
        {
            _moving.AtStun += OnAtStun;
            _moving.AtStunEnd += OnAtStunEnd;

            _pathfinding.MoveWith += MoveWith;
            _pathfinding.MoveTo += MoveTo;
            _pathfinding.Stop += Stop;
        }

        private void UnsubscribeFromEvents()
        {
            _moving.AtStun -= OnAtStun;
            _moving.AtStunEnd -= OnAtStunEnd;

            _pathfinding.MoveWith += MoveWith;
            _pathfinding.MoveTo -= MoveTo;
            _pathfinding.Stop += Stop;
        }
    }
}
