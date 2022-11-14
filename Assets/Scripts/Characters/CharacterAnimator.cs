using System;
using Characters.Common;
using Characters.Moving;
using Characters.Moving.Elements;
using Infrastructure.Installers.Game.Settings;
using UnityEngine;

namespace Characters
{
    public class CharacterAnimator
    {
        private const float MoveMotion = 1f;
        private const float IdleMotion = 0.5f;

        private static readonly int MoveX = Animator.StringToHash("moveX");
        private static readonly int MoveY = Animator.StringToHash("moveY");

        public event Action<AnimationStatus> UpdateFinish;

        public bool ChangeDirectionTimeAlternative { private get; set; }

        private readonly bool _ignoreChangeDirectionTime;

        private bool IsMoving => !_moving.Stopped && TargetDirection.magnitude >= GameSettings.Character.VelocityDelta;

        private Vector2 TargetDirection => Direction9Utils.AnyDirectionToSnappedVector2(_moving.TargetVelocity);

        private float TimeToChangeDirection => ChangeDirectionTimeAlternative == false
            ? GameSettings.Character.DirectionChangeTime
            : GameSettings.Character.DirectionChangeTimeAlternative;

        private bool EnoughTimeForDirectionChange =>
            Time.time - TimeToChangeDirection >= _directionChangeTime;

        private readonly AnimationStatus _status = new();

        private Vector2 _direction;
        private float _directionChangeTime = float.NegativeInfinity;

        private bool _lookToSomething;
        private Vector2 _lookDirection;

        private readonly Animator _animator;
        private readonly CharacterMoving _moving;

        public CharacterAnimator(Animator animator, CharacterMoving moving, CharacterType characterType)
        {
            _animator = animator;
            _moving = moving;
            _ignoreChangeDirectionTime = characterType == CharacterType.Hero;
        }

        public void Tick()
        {
            WatchForMoveDirectionsChanges();
            UpdateAnimations();
        }

        public void LookTo(Vector2 direction)
        {
            _lookToSomething = true;
            _lookDirection = direction;
        }

        public void Stun(bool value) { }

        public void SetBool(int hashName, bool value)
        {
            _animator.SetBool(hashName, value);
        }

        private void UpdateAnimations()
        {
            UpdateStatus();
            UpdateParameters();

            UpdateFinish?.Invoke(_status);
        }

        private void UpdateStatus()
        {
            _status.Motion = IsMoving ? MoveMotion : IdleMotion;

            if (IsMoving)
                _lookToSomething = false;

            if (_lookToSomething)
                _status.Direction = _lookDirection;
        }

        private void WatchForMoveDirectionsChanges()
        {
            if (TargetDirection.magnitude <= GameSettings.Character.VelocityDelta) return;

            UpdateDirection();
            UpdateOngoingDirection();
        }

        private void UpdateOngoingDirection()
        {
            if (_ignoreChangeDirectionTime ||
                (_direction == TargetDirection && EnoughTimeForDirectionChange))
            {
                _status.Direction = TargetDirection;
            }
        }

        private void UpdateDirection()
        {
            if (_direction == TargetDirection) return;

            _direction = TargetDirection;
            _directionChangeTime = Time.time;
        }

        private void UpdateParameters()
        {
            _animator.SetFloat(MoveX, _status.Velocity.x);
            _animator.SetFloat(MoveY, _status.Velocity.y);
        }

        public class AnimationStatus
        {
            public Vector2 Direction;
            public float Motion;

            public Vector2 Velocity => Direction * Motion;
        }
    }
}
