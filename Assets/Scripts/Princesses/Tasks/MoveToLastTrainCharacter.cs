using Characters.Moving;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Trains;
using Trains.Characters;

namespace Princesses.Tasks
{
    [Category("Princesses")]
    public class MoveToLastTrainCharacter : ActionTask<Princess>
    {
        private CharacterMoving _moving;
        private TrainCharacter _trainCharacter;
        private Train _train;

        protected override string OnInit()
        {
            _moving = agent.Moving;
            _trainCharacter = agent.TrainCharacter;
            _train = agent.Train;

            return null;
        }

        protected override void OnUpdate()
        {
            var direction = (_train.LastTrainCharacter.Position - agent.Position).normalized;

            if (IsNotCloseEnough())
                _moving.Move(direction);
            else
                EnterTrain();
        }

        private bool IsNotCloseEnough()
        {
            return (_train.LastTrainCharacter.Position - agent.Position).magnitude >
                   _train.LastTrainCharacter.RequiredDistanceToStop;
        }

        private void EnterTrain()
        {
            if (_trainCharacter.InTrainNow == false)
                _trainCharacter.BindToPrevious(_train.LastTrainCharacter);

            EndAction(true);
        }
    }
}
