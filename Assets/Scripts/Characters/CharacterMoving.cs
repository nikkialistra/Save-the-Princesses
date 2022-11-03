using Characters.Stats.Character;
using DG.Tweening;
using Infrastructure.Installers.Game.Settings;
using UnityEngine;
using Zenject;

namespace Characters
{
    [RequireComponent(typeof(CharacterStats))]
    [RequireComponent(typeof(CharacterPathfinding))]
    [RequireComponent(typeof(CharacterMovement))]
    [RequireComponent(typeof(Character))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterMoving : MonoBehaviour
    {
        public Vector2 TargetVelocity => _movement.TargetVelocity;

        public bool Stopped => _movement.Stopped;

        public float MovementSpeed => _stats.MovementSpeed.Value;

        private float AccelerationAmount => (MovementSpeed / _settings.AccelerationTime) * Time.fixedDeltaTime;
        private float DecelerationAmount => (MovementSpeed / _settings.DecelerationTime) * Time.fixedDeltaTime;

        private bool Blocked => _atKnockback || _stunned;

        private bool _atKnockback;
        private bool _stunned;

        private CharacterPathfinding _pathfinding;
        private CharacterMovement _movement;
        private Character _character;
        private Rigidbody2D _rigidBody2d;

        private CharacterStats _stats;

        private CharacterSettings _settings;

        [Inject]
        public void Construct(CharacterSettings settings)
        {
            _settings = settings;
        }

        public void Initialize()
        {
            FillComponents();

            _pathfinding.RepathRate = _settings.RepathRate;

            SubscribeToEvents();
        }

        public void Dispose()
        {
            UnsubscribeFromEvents();
        }

        private void FixedUpdate()
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
            var delta = destination - (Vector2)transform.position;

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

        private void FillComponents()
        {
            _pathfinding = GetComponent<CharacterPathfinding>();
            _movement = GetComponent<CharacterMovement>();
            _character = GetComponent<Character>();
            _rigidBody2d = GetComponent<Rigidbody2D>();

            _stats = GetComponent<CharacterStats>();
        }

        private void SubscribeToEvents()
        {
            _character.AtStun += OnAtStun;
            _character.AtStunEnd += OnAtStunEnd;

            _pathfinding.MoveWith += MoveWith;
            _pathfinding.MoveTo += MoveTo;
            _pathfinding.Stop += Stop;
        }

        private void UnsubscribeFromEvents()
        {
            _character.AtStun -= OnAtStun;
            _character.AtStunEnd -= OnAtStunEnd;

            _pathfinding.MoveWith += MoveWith;
            _pathfinding.MoveTo -= MoveTo;
            _pathfinding.Stop += Stop;
        }
    }
}
