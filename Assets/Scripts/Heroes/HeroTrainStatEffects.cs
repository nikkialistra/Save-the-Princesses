using System.Collections.Generic;
using Characters;
using GameData.Stats;

namespace Heroes
{
    public class HeroTrainStatEffects
    {
        private readonly List<Trait> _allEffects = new();

        private readonly Character _character;

        public HeroTrainStatEffects(Character character)
        {
            _character = character;
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
