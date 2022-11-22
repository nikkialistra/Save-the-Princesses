using System;
using GameData.Stats.Types;
using StatSystem;
using UnityEngine;

namespace Characters.Stats.Ranged
{
    [Serializable]
    public class RangedStats
    {
        public Stat<RangedStat> AttackSpeed => _attackSpeed;
        public Stat<RangedStat> AttackRange => _attackRange;
        public Stat<RangedStat> DamageMultiplier => _damageMultiplier;
        public Stat<RangedStat> KnockbackMultiplier => _knockbackMultiplier;
        public Stat<RangedStat> StunMultiplier => _stunMultiplier;

        [SerializeField] private Stat<RangedStat> _attackSpeed;
        [SerializeField] private Stat<RangedStat> _attackRange;
        [SerializeField] private Stat<RangedStat> _damageMultiplier;
        [SerializeField] private Stat<RangedStat> _knockbackMultiplier;
        [SerializeField] private Stat<RangedStat> _stunMultiplier;

        public RangedStats(InitialRangedStats initialStats)
        {
            _attackSpeed = new Stat<RangedStat>(initialStats.AttackSpeed);
            _attackRange = new Stat<RangedStat>(initialStats.AttackRange);
            _damageMultiplier = new Stat<RangedStat>(initialStats.DamageMultiplier);
            _knockbackMultiplier = new Stat<RangedStat>(initialStats.KnockbackMultiplier);
            _stunMultiplier = new Stat<RangedStat>(initialStats.StunMultiplier);
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
