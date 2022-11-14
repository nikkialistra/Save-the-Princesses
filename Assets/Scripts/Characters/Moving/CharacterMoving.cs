using System;
using Characters.Moving.Elements;
using Infrastructure.Installers.Game.Settings;
using Pathfinding;
using Pathfinding.RVO;
using UnityEngine;

namespace Characters.Moving
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Seeker))]
    [RequireComponent(typeof(RVOController))]
    [RequireComponent(typeof(LineRenderer))]
    public class CharacterMoving : MonoBehaviour
    {
        public event Action AtStun;
        public event Action AtStunEnd;

        public bool Active { private get; set; }

        public float MovementSpeed => _character.Stats.MovementSpeed;
        public Vector2 TargetVelocity => _moveCalculation.TargetVelocity;
        public bool Stopped => _movement.Stopped;

        public bool ShouldLocallyAvoid => _shouldLocallyAvoid;
        public bool ShowPath => _showPath;

        [SerializeField] private bool _shouldLocallyAvoid;
        [Space]
        [SerializeField] private bool _showPath;

        private CharacterMovement _movement;
        private CharacterPathfinding _pathfinding;
        private CharacterMoveCalculation _moveCalculation;

        private Rigidbody2D _rigidbody2D;
        private Seeker _seeker;
        private RVOController _rvoController;
        private LineRenderer _lineRenderer;

        private Character _character;

        public void Initialize(Character character)
        {
            _character = character;

            _rigidbody2D = GetComponent<Rigidbody2D>();
            _seeker = GetComponent<Seeker>();
            _rvoController = GetComponent<RVOController>();
            _lineRenderer = GetComponent<LineRenderer>();

            _movement = new CharacterMovement(_rigidbody2D);
            _pathfinding = new CharacterPathfinding(this, _seeker, _rvoController, _lineRenderer);
            _moveCalculation = new CharacterMoveCalculation(this, _rigidbody2D, _pathfinding, _movement);

            _pathfinding.RepathRate = GameSettings.Character.RepathRate;

            _character.AtStun += OnAtStun;
            _character.AtStunEnd += OnAtStunEnd;
        }

        public void Dispose()
        {
            _moveCalculation.Dispose();

            _character.AtStun -= OnAtStun;
            _character.AtStunEnd -= OnAtStunEnd;
        }

        public void Tick()
        {
            if (!Active) return;

            _pathfinding.Tick();
        }

        public void FixedTick()
        {
            if (!Active) return;

            _moveCalculation.FixedTick();
        }

        public void FindPathTo(Vector2 position)
        {
            _moveCalculation.FindPathTo(position);
        }

        public void Move(Vector2 direction)
        {
            _moveCalculation.Move(direction);
        }

        public void ResetMove()
        {
            _moveCalculation.ResetMove();
        }

        public void UpdateVelocity(float accelerationAmount, float decelerationAmount, float movementSpeed)
        {
            _movement.UpdateVelocity(accelerationAmount, decelerationAmount, movementSpeed);
        }

        public void MoveWithSpeed(Vector2 direction, float speed)
        {
            _movement.MoveWithSpeed(direction, speed);
        }

        public void Knockback(Vector2 value)
        {
            _moveCalculation.Knockback(value);
        }

        public void Stop()
        {
            _moveCalculation.Stop();
        }

        private void OnAtStun()
        {
            AtStun?.Invoke();
        }

        private void OnAtStunEnd()
        {
            AtStunEnd?.Invoke();
        }
    }
}
