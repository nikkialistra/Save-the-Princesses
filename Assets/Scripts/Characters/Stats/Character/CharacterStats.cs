using System;
using GameData.Stats.Types;
using StatSystem;

namespace Characters.Stats.Character
{
    public class CharacterStats
    {
        public Stat<CharacterStat> MaxHealth { get; }
        public Stat<CharacterStat> MovementSpeed { get; }
        public Stat<CharacterStat> Armor { get; }
        public Stat<CharacterStat> Vampirism { get; }
        public Stat<CharacterStat> Luck { get; }
        public Stat<CharacterStat> KnockbackStand { get; }
        public Stat<CharacterStat> StunResistance { get; }

        public CharacterStats(InitialCharacterStats initialStats)
        {
            MaxHealth = new Stat<CharacterStat>(initialStats.MaxHealth);
            MovementSpeed = new Stat<CharacterStat>(initialStats.MovementSpeed);
            Armor = new Stat<CharacterStat>(initialStats.Armor);
            Vampirism = new Stat<CharacterStat>(initialStats.Vampirism);
            Luck = new Stat<CharacterStat>(initialStats.Luck);
            KnockbackStand = new Stat<CharacterStat>(initialStats.KnockbackStand);
            StunResistance = new Stat<CharacterStat>(initialStats.StunResistance);
        }

        public void AddStatModifier(StatModifier<CharacterStat> statModifier)
        {
            var stat = ChooseStat(statModifier);

            stat.AddModifier(statModifier);
        }

        public void RemoveStatModifier(StatModifier<CharacterStat> statModifier)
        {
            var stat = ChooseStat(statModifier);

            stat.RemoveModifier(statModifier);
        }

        private Stat<CharacterStat> ChooseStat(StatModifier<CharacterStat> statModifier)
        {
            var stat = statModifier.StatType switch
            {
                CharacterStat.MovementSpeed => MovementSpeed,
                CharacterStat.MaxHealth => MaxHealth,
                CharacterStat.Armor => Armor,
                CharacterStat.Vampirism => Vampirism,
                CharacterStat.Luck => Luck,
                CharacterStat.KnockbackStand => KnockbackStand,
                CharacterStat.StunResistance => StunResistance,
                _ => throw new ArgumentOutOfRangeException()
            };
            return stat;
        }
    }
}
