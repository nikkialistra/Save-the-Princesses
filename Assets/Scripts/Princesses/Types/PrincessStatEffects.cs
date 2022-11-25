using GameData.Stats;
using Heroes;
using Trains.Characters;
using UnityEngine;

namespace Princesses.Types
{
    [RequireComponent(typeof(TrainCharacter))]
    public class PrincessStatEffects : MonoBehaviour
    {
        [SerializeField] private Trait _effects;

        private TrainCharacter _trainCharacter;
        private Hero _hero;

        public void Initialize(Hero hero)
        {
            _hero = hero;

            _trainCharacter = GetComponent<TrainCharacter>();

            _trainCharacter.TrainEnter += OnTrainEnter;
            _trainCharacter.TrainLeave += OnTrainLeave;
        }

        public void Dispose()
        {
            _trainCharacter.TrainEnter -= OnTrainEnter;
            _trainCharacter.TrainLeave -= OnTrainLeave;
        }

        private void OnTrainEnter()
        {
            _hero.ApplyPrincessStatEffects(_effects);
        }

        private void OnTrainLeave()
        {
            _hero.RemovePrincessStatEffects(_effects);
        }
    }
}
