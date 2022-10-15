using System;
using StatSystem;
using UnityEngine;

namespace Characters.Stats.Character
{
    public class CharacterStats : MonoBehaviour
    {
        public Stat<CharacterStat> MaxHealth => _maxHealth;
        public Stat<CharacterStat> MovementSpeed => _movementSpeed;
        public Stat<CharacterStat> Armor => _armor;
        public Stat<CharacterStat> Vampirism => _vampirism;
        public Stat<CharacterStat> Luck => _luck;
        public Stat<CharacterStat> KnockbackStand => _knockbackStand;
        public Stat<CharacterStat> StunResistance => _stunResistance;

        [SerializeField] private Stat<CharacterStat> _maxHealth;
        [SerializeField] private Stat<CharacterStat> _movementSpeed;
        [SerializeField] private Stat<CharacterStat> _armor;
        [SerializeField] private Stat<CharacterStat> _vampirism;
        [SerializeField] private Stat<CharacterStat> _luck;
        [SerializeField] private Stat<CharacterStat> _knockbackStand;
        [SerializeField] private Stat<CharacterStat> _stunResistance;

        public void Initialize()
        {
            InitializeStats();
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

        private void InitializeStats()
        {
            _maxHealth.Initialize();
            _movementSpeed.Initialize();
            _armor.Initialize();
            _vampirism.Initialize();
            _luck.Initialize();
            _knockbackStand.Initialize();
            _stunResistance.Initialize();
        }

        private Stat<CharacterStat> ChooseStat(StatModifier<CharacterStat> statModifier)
        {
            var stat = statModifier.StatType switch
            {
                CharacterStat.MovementSpeed => _movementSpeed,
                CharacterStat.MaxHealth => _maxHealth,
                CharacterStat.Armor => _armor,
                CharacterStat.Vampirism => _vampirism,
                CharacterStat.Luck => _luck,
                CharacterStat.KnockbackStand => _knockbackStand,
                CharacterStat.StunResistance => _stunResistance,
                _ => throw new ArgumentOutOfRangeException()
            };
            return stat;
        }
    }
}
