using Infrastructure.Installers.Game.Settings;
using NodeCanvas.Framework;
using ParadoxNotion;
using ParadoxNotion.Design;
using Pathfinding;
using Surrounding.Staging.Content;
using UnityEngine;

namespace Characters.Tasks
{
    [Category("Characters")]
    public class Wander : ActionTask<Character>
    {
        private const int MaxCollideCount = 7;

        public float DistanceMin = 5;
        public float DistanceMax = 10;

        public float DeviationStrength = 3;

        private Vector2 _destination;

        private int _collideCount;

        private CharacterMoving _moving;
        private CharacterSettings _settings;

        protected override string OnInit()
        {
            _moving = agent.Moving;
            _settings = agent.Settings;

            return null;
        }

        protected override void OnExecute()
        {
            if (!FindDestination())
            {
                EndAction(true);
                return;
            }

            _collideCount = 0;

            router.onTriggerStay2D += OnTriggerCollide2D;
        }

        protected override void OnUpdate()
        {
            CheckForFinish();
        }

        protected override void OnStop()
        {
            router.onTriggerStay2D -= OnTriggerCollide2D;

            _moving.ResetMove();
        }

        private bool FindDestination()
        {
            for (int i = 0; i < _settings.DestinationSearchTriesCount; i++)
            {
                var randomDirection = agent.Forward + (Random.insideUnitCircle * DeviationStrength);
                var randomPosition = agent.Position + randomDirection * Random.Range(DistanceMin, DistanceMax);

                var target = (Vector3)AstarPath.active.GetNearest(randomPosition, NNConstraint.Default).node.position;

                if (agent.Room.InBounds(target))
                {
                    _destination = target;
                    _moving.FindPathTo(_destination);
                    return true;
                }
            }

            return false;
        }

        private void OnTriggerCollide2D(EventData<Collider2D> eventData)
        {
            if (eventData.value.TryGetComponent<Walls>(out _) == false) return;

            _collideCount++;

            if (_collideCount >= MaxCollideCount)
                EndAction(true);
        }

        private void CheckForFinish()
        {
            if (Vector2.Distance(agent.Position, _destination) <= _settings.DestinationDistanceDeltaPathfinding)
                EndAction(true);
        }
    }
}
