using System.Collections.Generic;
using Characters;
using Characters.Traits;
using UnityEngine;

namespace Heroes
{
    [RequireComponent(typeof(Character))]
    public class HeroTrainStatEffects : MonoBehaviour
    {
        private List<Trait> _allEffects = new();

        private Character _character;

        public void Initialize()
        {
            _character = GetComponent<Character>();
        }

        public void ApplyPrincessStatEffects(Trait effects)
        {
            _allEffects.Add(effects);

            _character.AddTrait(effects);
        }

        public void RemovePrincessStatEffects(Trait effects)
        {
            _allEffects.Remove(effects);

            _character.RemoveTrait(effects);
        }
    }
}
