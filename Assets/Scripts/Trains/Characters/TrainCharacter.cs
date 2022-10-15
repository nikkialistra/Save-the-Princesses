﻿using System;
using Characters;
using Infrastructure.CompositionRoot.Settings;
using TMPro;
using UnityEngine;
using Zenject;

namespace Trains.Characters
{
    [RequireComponent(typeof(CharacterMoving))]
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
            ? _settings.DistanceToHeroToMove
            : _settings.DistanceBetweenPrincessesToMove;

        public float RequiredDistanceToStop => IsHero
            ? _settings.DistanceToHeroToStop
            : _settings.DistanceBetweenPrincessesToStop;

        [SerializeField] private float _handsYPosition = 0.4f;
        [SerializeField] private TMP_Text _orderNumber;

        private Train _train;
        private TrainCharacter _next;

        private CharacterMoving _moving;

        private PrincessSettings _settings;

        [Inject]
        public void Construct(Train train, PrincessSettings settings)
        {
            _train = train;
            _settings = settings;
        }

        public void Initialize()
        {
            _moving = GetComponent<CharacterMoving>();
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