using System;
using StatSystem;
using UnityEngine;

namespace Characters.Stats.Melee
{
    public class MeleeStats : MonoBehaviour
    {
        public Stat<MeleeStat> AttackSpeed => _attackSpeed;
        public Stat<MeleeStat> DamageMultiplier => _damageMultiplier;

        [SerializeField] private Stat<MeleeStat> _attackSpeed;
        [SerializeField] private Stat<MeleeStat> _damageMultiplier;

        public void Initialize()
        {
            InitializeStats();
        }

        private void InitializeStats()
        {
            _attackSpeed.Initialize();
            _damageMultiplier.Initialize();
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

                MeleeStat.AttackSpeed => _attackSpeed,
                MeleeStat.DamageMultiplier => _damageMultiplier,
                _ => throw new ArgumentOutOfRangeException()
            };
            return stat;
        }
    }
}
