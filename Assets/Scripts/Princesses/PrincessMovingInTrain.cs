using Characters;
using Characters.Moving;
using Characters.Stats;
using GameData.Settings;
using Heroes;
using Trains.Characters;
using UnityEngine;

namespace Princesses
{
    [RequireComponent(typeof(MovingInTrainPathfinding))]
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

        public void Initialize(CharacterMoving moving, AllStats stats, Hero hero)
        {
            _pathfinding = GetComponent<MovingInTrainPathfinding>();
            _pathfinding.Initialize(hero);

            _moving = moving;
            _stats = stats;
        }

        public void FixedTick()
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

        private void MoveWithSpeed(Vector2 direction, float speed)
        {
            _moving.MoveWithSpeed(direction, speed);
        }
    }
}
