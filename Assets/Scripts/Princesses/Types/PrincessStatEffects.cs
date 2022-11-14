using GameData.Stats;
using Heroes;
using Trains.Characters;
using UnityEngine;
using Zenject;

namespace Princesses.Types
{
    [RequireComponent(typeof(TrainCharacter))]
    public class PrincessStatEffects : MonoBehaviour
    {
        [SerializeField] private Trait _effects;

        private TrainCharacter _trainCharacter;
        private Hero _hero;

        [Inject]
        public void Construct(Hero hero)
        {
            _hero = hero;
        }

        public void Initialize()
        {
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
