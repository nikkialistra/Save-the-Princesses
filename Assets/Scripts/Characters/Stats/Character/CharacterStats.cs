using System;
using GameData.Stats.Types;
using Sirenix.OdinInspector;
using StatSystem;
using UnityEngine;

namespace Characters.Stats.Character
{
    [Serializable]
    public class CharacterStats
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

        public CharacterStats(InitialCharacterStats initialStats)
        {
            _maxHealth = new Stat<CharacterStat>(initialStats.MaxHealth);
            _movementSpeed = new Stat<CharacterStat>(initialStats.MovementSpeed);
            _armor = new Stat<CharacterStat>(initialStats.Armor);
            _vampirism = new Stat<CharacterStat>(initialStats.Vampirism);
            _luck = new Stat<CharacterStat>(initialStats.Luck);
            _knockbackStand = new Stat<CharacterStat>(initialStats.KnockbackStand);
            _stunResistance = new Stat<CharacterStat>(initialStats.StunResistance);
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
