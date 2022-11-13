using System;
using GameData.Stats.Types;
using StatSystem;

namespace Characters.Stats.Ranged
{
    public class RangedStats
    {
        public Stat<RangedStat> AttackSpeed { get; }
        public Stat<RangedStat> AttackRange { get; }
        public Stat<RangedStat> DamageMultiplier { get; }

        public RangedStats(InitialRangedStats initialStats)
        {
            AttackSpeed = new Stat<RangedStat>(initialStats.AttackSpeed);
            AttackRange = new Stat<RangedStat>(initialStats.AttackRange);
            DamageMultiplier = new Stat<RangedStat>(initialStats.DamageMultiplier);
        }

        public void AddStatModifier(StatModifier<RangedStat> statModifier)
        {
            var stat = ChooseStat(statModifier);

            stat.AddModifier(statModifier);
        }

        public void RemoveStatModifier(StatModifier<RangedStat> statModifier)
        {
            var stat = ChooseStat(statModifier);

            stat.RemoveModifier(statModifier);
        }

        private Stat<RangedStat> ChooseStat(StatModifier<RangedStat> statModifier)
        {
            var stat = statModifier.StatType switch
            {
                RangedStat.AttackSpeed => AttackSpeed,
                RangedStat.AttackRange => AttackRange,
                RangedStat.DamageMultiplier => DamageMultiplier,
                _ => throw new ArgumentOutOfRangeException()
            };
            return stat;
        }
    }
}
