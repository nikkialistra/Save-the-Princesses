using System;
using GameData.Stats.Types;
using StatSystem;

namespace Characters.Stats.Melee
{
    public class MeleeStats
    {
        public Stat<MeleeStat> AttackSpeed { get; }
        public Stat<MeleeStat> DamageMultiplier { get; }

        public MeleeStats(InitialMeleeStats initialStats)
        {
            AttackSpeed = new Stat<MeleeStat>(initialStats.AttackSpeed);
            DamageMultiplier = new Stat<MeleeStat>(initialStats.DamageMultiplier);
        }

        public void AddStatModifier(StatModifier<MeleeStat> statModifier)
        {
            var stat = ChooseStat(statModifier);

            stat.AddModifier(statModifier);
        }

        public void RemoveStatModifier(StatModifier<MeleeStat> statModifier)
        {
            var stat = ChooseStat(statModifier);

            stat.RemoveModifier(statModifier);
        }

        private Stat<MeleeStat> ChooseStat(StatModifier<MeleeStat> statModifier)
        {
            var stat = statModifier.StatType switch
            {

                MeleeStat.AttackSpeed => AttackSpeed,
                MeleeStat.DamageMultiplier => DamageMultiplier,
                _ => throw new ArgumentOutOfRangeException()
            };
            return stat;
        }
    }
}
