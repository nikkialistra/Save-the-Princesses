using System;
using System.Collections.Generic;
using Heroes;
using Sirenix.OdinInspector;
using Trains.Characters;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Trains
{
    public class Train : MonoBehaviour
    {
        public TrainCharacter LastTrainCharacter => _trainCharacters[^1];

        private readonly List<TrainCharacter> _trainCharacters = new();

        private TrainCharacter _trainHero;

        private Hero _hero;
        private PlayerInput _playerInput;

        private TrainInput _input;

        [Inject]
        public void Construct(PlayerInput playerInput)
        {
            _playerInput = playerInput;
        }

        public void Initialize(Hero hero)
        {
            _hero = hero;

            _input = new TrainInput(this, _playerInput);

            _trainHero = _hero.TrainCharacter;
            _trainCharacters.Add(_trainHero);
        }

        public void Dispose()
        {
            _trainCharacters.Clear();
            _input.Dispose();
        }

        public void Recalculate()
        {
            _trainCharacters.Clear();
            Fill();
        }

        [Button]
        public void Steal(TrainCharacter trainCharacter)
        {
            if (trainCharacter.IsHero)
                throw new InvalidOperationException("Hero should not be stolen from train");

            if (_trainCharacters.Contains(trainCharacter))
                throw new InvalidOperationException($"Train doesn't have {trainCharacter}");
        }

        [Button]
        public void RemoveAt(int index)
        {
            if (index == 0)
                throw new InvalidOperationException("Hero should not be removed from train");

            if (index >= _trainCharacters.Count)
                throw new InvalidOperationException($"Train length is less than or equal to {index}");

            _trainCharacters[index].Leave();
        }

        public void ShowOrderNumbers()
        {
            var orderNumber = 1;
            ForEachTrainPrincess(ShowOrderNumber);

            void ShowOrderNumber(TrainCharacter trainPrincess)
            {
                trainPrincess.ShowOrderNumber(orderNumber);
                orderNumber++;
            }
        }

        public void HideOrderNumbers()
        {
            ForEachTrainPrincess(HideOrderNumber);

            void HideOrderNumber(TrainCharacter trainPrincess) => trainPrincess.HideOrderNumber();
        }

        private void Fill()
        {
            _trainCharacters.Add(_trainHero);
            ForEachTrainPrincess(Add);

            void Add(TrainCharacter trainPrincess) => _trainCharacters.Add(trainPrincess);
        }

        private void ForEachTrainPrincess(Action<TrainCharacter> action)
        {
            var trainPrincess = _trainHero.Next;

            while (trainPrincess != null)
            {
                action(trainPrincess);
                trainPrincess = trainPrincess.Next;
            }
        }
    }
}
