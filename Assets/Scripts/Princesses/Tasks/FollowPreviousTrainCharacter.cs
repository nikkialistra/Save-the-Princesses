using Characters;
using Characters.Moving;
using Characters.Moving.Elements;
using Infrastructure.Installers.Game.Settings;
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

        private PrincessSettings _settings;

        protected override string OnInit()
        {
            _movingInTrain = agent.MovingInTrain;
            _trainCharacter = agent.TrainCharacter;

            _moving = agent.Moving;
            _settings = agent.Settings;

            return null;
        }

        protected override void OnExecute()
        {
            _moving.Active = false;
            _movingInTrain.enabled = true;
            _trainCharacter.TrainLeave += OnTrainLeave;
        }

        protected override void OnStop()
        {
            _moving.Active = true;
            _movingInTrain.enabled = false;
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
            var reachingLimitDistance = Distance > _trainCharacter.Previous.DistanceToMove * _settings.DistancePercentForElevatedSpeed;

            var aboveHandsTensionDistance = Distance > _trainCharacter.Previous.DistanceToMove * _settings.DistancePercentForHeroSpeed;
            var belowHandsTensionDistance = Distance < _trainCharacter.Previous.DistanceToMove * _settings.DistancePercentForRegularSpeed;

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
