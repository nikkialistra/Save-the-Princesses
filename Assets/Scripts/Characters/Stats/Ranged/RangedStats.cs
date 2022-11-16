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
        public Stat<RangedStat> KnockbackMultiplier { get; }
        public Stat<RangedStat> StunMultiplier { get; }

        public RangedStats(InitialRangedStats initialStats)
        {
            AttackSpeed = new Stat<RangedStat>(initialStats.AttackSpeed);
            AttackRange = new Stat<RangedStat>(initialStats.AttackRange);
            DamageMultiplier = new Stat<RangedStat>(initialStats.DamageMultiplier);
            KnockbackMultiplier = new Stat<RangedStat>(initialStats.KnockbackMultiplier);
            StunMultiplier = new Stat<RangedStat>(initialStats.StunMultiplier);
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
                RangedStat.KnockbackMultiplier => KnockbackMultiplier,
                RangedStat.StunMultiplier => StunMultiplier,
                _ => throw new ArgumentOutOfRangeException()
            };
            return stat;
        }
    }
}
