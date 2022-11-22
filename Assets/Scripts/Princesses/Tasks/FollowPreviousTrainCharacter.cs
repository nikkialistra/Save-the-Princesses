using Characters.Moving;
using GameData.Settings;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Trains.Characters;

namespace Princesses.Tasks
{
    [Category("Princesses")]
    public class FollowPreviousTrainCharacter : ActionTask<Princess>
    {
        private float Distance => (_trainCharacter.Previous.Position - agent.Position).magnitude;

        private PrincessMovingInTrain _movingInTrain;
        private TrainCharacter _trainCharacter;

        private CharacterMoving _moving;

        protected override string OnInit()
        {
            _movingInTrain = agent.MovingInTrain;
            _trainCharacter = agent.TrainCharacter;

            _moving = agent.Moving;

            return null;
        }

        protected override void OnExecute()
        {
            _trainCharacter.TrainLeave += OnTrainLeave;
        }

        protected override void OnStop()
        {
            _trainCharacter.TrainLeave -= OnTrainLeave;
        }

        protected override void OnUpdate()
        {
            ChangeSpeedBasedOnDistance();

            if (ShouldFollow())
                _movingInTrain.MoveTo(_trainCharacter.Previous.Position);

            if (ShouldStop())
                _movingInTrain.Stop();
        }

        private void OnTrainLeave()
        {
            _movingInTrain.Stop();
            EndAction(false);
        }

        private bool ShouldFollow()
        {
            return _trainCharacter.Previous.Moving || Distance > _trainCharacter.Previous.DistanceToMove;
        }

        private bool ShouldStop()
        {
            return Distance <= _trainCharacter.Previous.RequiredDistanceToStop;
        }

        private void ChangeSpeedBasedOnDistance()
        {
            var reachingLimitDistance = Distance > _trainCharacter.Previous.DistanceToMove * GameSettings.Princess.DistancePercentForElevatedSpeed;

            var aboveHandsTensionDistance = Distance > _trainCharacter.Previous.DistanceToMove * GameSettings.Princess.DistancePercentForHeroSpeed;
            var belowHandsTensionDistance = Distance < _trainCharacter.Previous.DistanceToMove * GameSettings.Princess.DistancePercentForRegularSpeed;

            if (reachingLimitDistance)
                agent.ElevateSpeed();
            else
                agent.DeElevateSpeed();

            if (aboveHandsTensionDistance)
                agent.ChangeToHeroSpeed();

            if (belowHandsTensionDistance)
                agent.ChangeToRegularSpeed();
        }

        private void TurnOffColliderStuck()
        {
            if (CheckForStuck()) return;

            agent.TurnOffCollider();
        }

        private bool CheckForStuck()
        {
            var reachingMaxDistance = Distance < _trainCharacter.Previous.DistanceToMove * 0.9f;
            var barelyMoving = agent.ActualVelocity <= 0.02f;

            return reachingMaxDistance && barelyMoving;
        }
    }
}
