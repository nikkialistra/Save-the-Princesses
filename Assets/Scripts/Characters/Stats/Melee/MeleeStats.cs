using System;
using GameData.Stats.Types;
using StatSystem;
using UnityEngine;

namespace Characters.Stats.Melee
{
    [Serializable]
    public class MeleeStats
    {
        public Stat<MeleeStat> AttackSpeed => _attackSpeed;
        public Stat<MeleeStat> DamageMultiplier => _damageMultiplier;
        public Stat<MeleeStat> KnockbackMultiplier => _knockbackMultiplier;
        public Stat<MeleeStat> StunMultiplier => _stunMultiplier;

        [SerializeField] private Stat<MeleeStat> _attackSpeed;
        [SerializeField] private Stat<MeleeStat> _damageMultiplier;
        [SerializeField] private Stat<MeleeStat> _knockbackMultiplier;
        [SerializeField] private Stat<MeleeStat> _stunMultiplier;

        public MeleeStats(InitialMeleeStats initialStats)
        {
            _attackSpeed = new Stat<MeleeStat>(initialStats.AttackSpeed);
            _damageMultiplier = new Stat<MeleeStat>(initialStats.DamageMultiplier);
            _knockbackMultiplier = new Stat<MeleeStat>(initialStats.KnockbackMultiplier);
            _stunMultiplier = new Stat<MeleeStat>(initialStats.StunMultiplier);
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
