using DG.Tweening;
using GameData.Settings;
using UnityEngine;

namespace Characters.Moving.Elements
{
    public class CharacterMoveCalculation
    {
        public Vector2 TargetVelocity => _movement.TargetVelocity;

        private float MovementSpeed => _moving.MovementSpeed;

        private float AccelerationAmount => (MovementSpeed / GameSettings.Character.AccelerationTime) * Time.fixedDeltaTime;
        private float DecelerationAmount => (MovementSpeed / GameSettings.Character.DecelerationTime) * Time.fixedDeltaTime;

        private bool Blocked => _atKnockback || _stunned;

        private bool _atKnockback;
        private bool _stunned;

        private readonly CharacterMoving _moving;
        private readonly Rigidbody2D _rigidBody2D;
        private readonly CharacterPathfinding _pathfinding;
        private readonly CharacterMovement _movement;

        public CharacterMoveCalculation(CharacterMoving moving, Rigidbody2D rigidbody2D,
            CharacterPathfinding pathfinding, CharacterMovement movement)
        {
            _moving = moving;
            _rigidBody2D = rigidbody2D;
            _pathfinding = pathfinding;
            _movement = movement;

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

            var targetPosition = _rigidBody2D.position + value;

            DOTween.To(() => _rigidBody2D.position, x => _rigidBody2D.position = x, targetPosition,
                    GameSettings.Character.TimeAtKnockback)
                .SetEase(Ease.OutQuint)
                .OnComplete(ResetKnockback);
        }

        private void MoveWith(Vector2 velocity)
        {
            if (velocity.magnitude <= GameSettings.Character.VelocityDelta)
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

            if (delta.magnitude <= GameSettings.Character.DestinationDistanceDelta)
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

        private void OnStunChange(bool status)
        {
            _stunned = status;
        }

        private void SubscribeToEvents()
        {
            _moving.StunChange += OnStunChange;

            _pathfinding.MoveWith += MoveWith;
            _pathfinding.MoveTo += MoveTo;
            _pathfinding.Stop += Stop;
        }

        private void UnsubscribeFromEvents()
        {
            _moving.StunChange -= OnStunChange;

            _pathfinding.MoveWith += MoveWith;
            _pathfinding.MoveTo -= MoveTo;
            _pathfinding.Stop += Stop;
        }
    }
}
