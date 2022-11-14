using Characters;
using Characters.Moving;
using Characters.Stats;
using Infrastructure.Installers.Game.Settings;
using Trains.Characters;
using UnityEngine;

namespace Princesses
{
    [RequireComponent(typeof(MovingInTrainPathfinding))]
    [RequireComponent(typeof(TrainCharacter))]
    public class PrincessMovingInTrain : MonoBehaviour
    {
        private float MovementSpeed => _stats.MovementSpeed;

        private float AccelerationAmount => (MovementSpeed / GameSettings.Princess.AccelerationTime) * Time.fixedDeltaTime;
        private float DecelerationAmount => (MovementSpeed / GameSettings.Princess.DecelerationTime) * Time.fixedDeltaTime;

        private MovingInTrainPathfinding _pathfinding;

        private CharacterMoving _moving;
        private TrainCharacter _trainCharacter;
        private CharacterAnimator _animator;

        private AllStats _stats;

        public void Initialize(CharacterMoving moving, CharacterAnimator animator, AllStats stats)
        {
            _moving = moving;
            _animator = animator;
            _stats = stats;

            InitializeComponents();

            _trainCharacter.TrainEnter += OnTrainEnter;
            _trainCharacter.TrainLeave += OnTrainLeave;
        }

        public void Dispose()
        {
            _trainCharacter.TrainEnter += OnTrainEnter;
            _trainCharacter.TrainLeave += OnTrainLeave;
        }

        private void FixedUpdate()
        {
            _moving.UpdateVelocity(AccelerationAmount, DecelerationAmount, MovementSpeed);
        }

        public void MoveTo(Vector2 target)
        {
            var delta = target - (Vector2)transform.position;

            if (delta.magnitude <= GameSettings.Character.DestinationDistanceDelta)
            {
                Stop();
                return;
            }

            if (_moving.Stopped)
                _pathfinding.ResetScanning();

            MoveWithSpeed(_pathfinding.FindDirectionToHero(), MovementSpeed);
        }

        public void Stop()
        {
            _moving.Stop();
        }

        private void OnTrainEnter()
        {
            _animator.ChangeDirectionTimeAlternative = true;
        }

        private void OnTrainLeave()
        {
            _animator.ChangeDirectionTimeAlternative = false;
        }

        private void MoveWithSpeed(Vector2 direction, float speed)
        {
            _moving.MoveWithSpeed(direction, speed);
        }

        private void InitializeComponents()
        {
            _pathfinding = GetComponent<MovingInTrainPathfinding>();
            _trainCharacter = GetComponent<TrainCharacter>();
            _animator = GetComponent<CharacterAnimator>();
        }
    }
}
