using System;
using Characters.Moving;
using Infrastructure.Installers.Game.Settings;
using TMPro;
using UnityEngine;
using Zenject;

namespace Trains.Characters
{
    public class TrainCharacter : MonoBehaviour
    {
        public event Action TrainEnter;
        public event Action TrainLeave;

        public event Action NextChange;

        public bool IsHero { get; private set; }
        public bool InTrainNow => IsHero || Previous != null;

        public TrainCharacter Next
        {
            get => _next;
            private set
            {
                _next = value;
                NextChange?.Invoke();
            }
        }
        public TrainCharacter Previous { get; private set; }

        public Vector2 Position => transform.position;
        public Vector2 PositionCenter => (Vector2)transform.position + new Vector2(0, _handsYPosition);

        public bool Moving => !_moving.Stopped;

        public float DistanceToMove => IsHero
            ? GameSettings.Princess.DistanceToHeroToMove
            : GameSettings.Princess.DistanceBetweenPrincessesToMove;

        public float RequiredDistanceToStop => IsHero
            ? GameSettings.Princess.DistanceToHeroToStop
            : GameSettings.Princess.DistanceBetweenPrincessesToStop;

        [SerializeField] private float _handsYPosition = 0.4f;
        [SerializeField] private TMP_Text _orderNumber;

        private Train _train;
        private TrainCharacter _next;

        private CharacterMoving _moving;

        [Inject]
        public void Construct(Train train)
        {
            _train = train;
        }

        public void Initialize(CharacterMoving moving)
        {
            _moving = moving;
        }

        public void SetAsHero()
        {
            IsHero = true;
        }

        public void BindToPrevious(TrainCharacter previous)
        {
            previous.TakeNext(this);
            Previous = previous;

            _train.Recalculate();

            TrainEnter?.Invoke();
        }

        public void Leave()
        {
            if (Next != null)
                BindTrainCharactersAround();
            else
                LeavePrevious();

            TrainLeave?.Invoke();
        }

        private void BindTrainCharactersAround()
        {
            Previous.TakeNext(Next);
            Next.TakePrevious(Previous);

            Previous = null;
            Next = null;

            _train.Recalculate();
        }

        private void LeavePrevious()
        {
            Previous.TakeOffNext();
            Previous = null;

            _train.Recalculate();
        }

        public void ShowOrderNumber(int value)
        {
            _orderNumber.enabled = true;
            _orderNumber.text = value.ToString();
        }

        public void HideOrderNumber()
        {
            _orderNumber.enabled = false;
        }

        private void TakePrevious(TrainCharacter previous)
        {
            Previous = previous;
        }

        private void TakeNext(TrainCharacter next)
        {
            Next = next;
        }

        private void TakeOffPrevious()
        {
            Previous = null;
        }

        private void TakeOffNext()
        {
            Next = null;
        }
    }
}
