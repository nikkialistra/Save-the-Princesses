using Characters.Stats.Character;
using Characters.Stats.Melee;
using Characters.Stats.Ranged;

namespace Characters.Stats
{
    public readonly struct AllStats
    {
        public CharacterStats CharacterStats { get; }
        public MeleeStats MeleeStats { get; }
        public RangedStats RangedStats { get; }

        public AllStats(CharacterStats characterStats, MeleeStats meleeStats, RangedStats rangedStats)
        {
            CharacterStats = characterStats;
            MeleeStats = meleeStats;
            RangedStats = rangedStats;
        }
    }
}
