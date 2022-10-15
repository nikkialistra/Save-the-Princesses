using Characters;
using Characters.Stats;
using Characters.Stats.Character;
using Infrastructure.CompositionRoot.Settings;
using Trains.Characters;
using UnityEngine;
using Zenject;

namespace Princesses
{
    [RequireComponent(typeof(MovingInTrainPathfinding))]
    [RequireComponent(typeof(CharacterMovement))]
    [RequireComponent(typeof(CharacterStats))]
    [RequireComponent(typeof(CharacterPathfinding))]
    [RequireComponent(typeof(CharacterAnimator))]
    [RequireComponent(typeof(TrainCharacter))]
    public class PrincessMovingInTrain : MonoBehaviour
    {
        private float MovementSpeed => _stats.MovementSpeed.Value;

        private float AccelerationAmount => (MovementSpeed / _princessSettings.AccelerationTime) * Time.fixedDeltaTime;
        private float DecelerationAmount => (MovementSpeed / _princessSettings.DecelerationTime) * Time.fixedDeltaTime;

        private MovingInTrainPathfinding _pathfinding;

        private CharacterMovement _movement;
        private TrainCharacter _trainCharacter;
        private CharacterAnimator _animator;

        private CharacterStats _stats;

        private PrincessSettings _princessSettings;
        private CharacterSettings _characterSettings;

        [Inject]
        public void Construct(PrincessSettings princessSettings, CharacterSettings characterSettings)
        {
            _princessSettings = princessSettings;
            _characterSettings = characterSettings;
        }

        public void Initialize()
        {
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
            _movement.UpdateVelocity(AccelerationAmount, DecelerationAmount, MovementSpeed);
        }

        public void MoveTo(Vector2 target)
        {
            var delta = target - (Vector2)transform.position;

            if (delta.magnitude <= _characterSettings.DestinationDistanceDelta)
            {
                Stop();
                return;
            }

            if (_movement.Stopped)
                _pathfinding.ResetScanning();

            MoveWithSpeed(_pathfinding.FindDirectionToHero(), MovementSpeed);
        }

        public void Stop()
        {
            _movement.Stop();
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
            _movement.MoveWithSpeed(direction, speed);
        }

        private void InitializeComponents()
        {
            _pathfinding = GetComponent<MovingInTrainPathfinding>();
            _movement = GetComponent<CharacterMovement>();
            _trainCharacter = GetComponent<TrainCharacter>();
            _animator = GetComponent<CharacterAnimator>();

            _stats = GetComponent<CharacterStats>();
        }
    }
}
