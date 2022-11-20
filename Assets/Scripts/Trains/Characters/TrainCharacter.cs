using System;
using Characters;
using Characters.Moving;
using GameData.Settings;
using Sirenix.OdinInspector;
using TMPro;
using Trains.HandConnections;
using UnityEngine;

namespace Trains.Characters
{
    public class TrainCharacter : MonoBehaviour
    {
        public event Action TrainEnter;
        public event Action TrainLeave;

        public event Action NextChange;

        public bool IsHero => _isHero;
        public bool InTrainNow => _isHero || Previous != null;

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

        public Vector2 Position => _character.Position;
        public Vector2 PositionCenter => _character.Position + new Vector2(0, HandsDeltaY);

        public bool Moving => !_moving.Stopped;

        public float DistanceToMove => _isHero
            ? GameSettings.Princess.DistanceToHeroToMove
            : GameSettings.Princess.DistanceBetweenPrincessesToMove;

        public float RequiredDistanceToStop => _isHero
            ? GameSettings.Princess.DistanceToHeroToStop
            : GameSettings.Princess.DistanceBetweenPrincessesToStop;

        [SerializeField] private bool _isHero;

        [ShowIf("@_isHero == false")]
        [SerializeField] private TMP_Text _orderNumber;
        [ShowIf("@_isHero == false")]
        [SerializeField] private Hands _hands;

        private float HandsDeltaY => _isHero ? GameSettings.Hero.HandsDeltaY : GameSettings.Princess.HandsDeltaY;

        private TrainCharacter _next;

        private Character _character;
        private CharacterMoving _moving;

        private Train _train;

        public void Initialize(Character character, CharacterMoving moving, Train train)
        {
            _character = character;
            _moving = moving;
            _train = train;

            if (!_isHero)
                _hands.Initialize(this);
        }

        public void Dispose()
        {
            _hands.Dispose();
        }

        public void Tick()
        {
            _hands.Tick();
        }

        public void BindToPrevious(TrainCharacter previous)
        {
            previous.TakeNext(this);
            Previous = previous;

            _train.Recalculate();

            _character.DirectionChangeTime = GameSettings.Character.DirectionChangeTimeInTrain;
            TrainEnter?.Invoke();
        }

        public void Leave()
        {
            if (Next != null)
                BindTrainCharactersAround();
            else
                LeavePrevious();

            _character.DirectionChangeTime = GameSettings.Character.DirectionChangeTime;
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
