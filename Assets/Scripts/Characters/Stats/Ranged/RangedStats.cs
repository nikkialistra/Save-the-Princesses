using System;
using StatSystem;
using UnityEngine;

namespace Characters.Stats.Ranged
{
    public class RangedStats : MonoBehaviour
    {
        public Stat<RangedStat> AttackSpeed => _attackSpeed;
        public Stat<RangedStat> AttackRange => _attackRange;
        public Stat<RangedStat> DamageMultiplier => _damageMultiplier;

        [SerializeField] private Stat<RangedStat> _attackSpeed;
        [SerializeField] private Stat<RangedStat> _attackRange;
        [SerializeField] private Stat<RangedStat> _damageMultiplier;

        public void Initialize()
        {
            InitializeStats();
        }

        private void InitializeStats()
        {
            _attackSpeed.Initialize();
            _attackRange.Initialize();
            _damageMultiplier.Initialize();
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
                RangedStat.AttackSpeed => _attackSpeed,
                RangedStat.AttackRange => _attackRange,
                RangedStat.DamageMultiplier => _damageMultiplier,
                _ => throw new ArgumentOutOfRangeException()
            };
            return stat;
        }
    }
}
