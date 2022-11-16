using System;
using GameData.Stats.Types;
using StatSystem;

namespace Characters.Stats.Melee
{
    public class MeleeStats
    {
        public Stat<MeleeStat> AttackSpeed { get; }
        public Stat<MeleeStat> DamageMultiplier { get; }
        public Stat<MeleeStat> KnockbackMultiplier { get; }
        public Stat<MeleeStat> StunMultiplier { get; }

        public MeleeStats(InitialMeleeStats initialStats)
        {
            AttackSpeed = new Stat<MeleeStat>(initialStats.AttackSpeed);
            DamageMultiplier = new Stat<MeleeStat>(initialStats.DamageMultiplier);
            KnockbackMultiplier = new Stat<MeleeStat>(initialStats.KnockbackMultiplier);
            StunMultiplier = new Stat<MeleeStat>(initialStats.StunMultiplier);
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
                MeleeStat.KnockbackMultiplier => KnockbackMultiplier,
                MeleeStat.StunMultiplier => StunMultiplier,
                _ => throw new ArgumentOutOfRangeException()
            };
            return stat;
        }
    }
}
