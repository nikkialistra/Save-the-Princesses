using Characters.Stats.Character;
using Characters.Stats.Melee;
using Characters.Stats.Ranged;
using GameData.Stats;
using StatSystem;

namespace Characters.Stats
{
    public class AllStats
    {
        public float MaxHealth => _characterStats.MaxHealth.Value;
        public float MovementSpeed => _characterStats.MovementSpeed.Value;
        public float Armor => _characterStats.Armor.Value;
        public float Vampirism => _characterStats.Vampirism.Value;
        public float Luck => _characterStats.Luck.Value;
        public float KnockbackStand => _characterStats.KnockbackStand.Value;
        public float StunResistance => _characterStats.StunResistance.Value;

        public float MeleeAttackSpeed => _meleeStats.AttackSpeed.Value;
        public float MeleeDamageMultiplier => _meleeStats.DamageMultiplier.Value;

        public float RangedAttackSpeed => _rangedStats.AttackSpeed.Value;
        public float RangedAttackRange => _rangedStats.AttackRange.Value;
        public float RangedDamageMultiplier => _rangedStats.DamageMultiplier.Value;

        public Stat<CharacterStat> MaxHealthStat => _characterStats.MaxHealth;
        public Stat<CharacterStat> MovementSpeedStat => _characterStats.MovementSpeed;
        public Stat<CharacterStat> ArmorStat => _characterStats.Armor;
        public Stat<CharacterStat> VampirismStat => _characterStats.Vampirism;
        public Stat<CharacterStat> LuckStat => _characterStats.Luck;
        public Stat<CharacterStat> KnockbackStandStat => _characterStats.KnockbackStand;
        public Stat<CharacterStat> StunResistanceStat => _characterStats.StunResistance;

        public Stat<MeleeStat> MeleeAttackSpeedStat => _meleeStats.AttackSpeed;
        public Stat<MeleeStat> MeleeDamageMultiplierStat => _meleeStats.DamageMultiplier;

        public Stat<RangedStat> RangedAttackSpeedStat => _rangedStats.AttackSpeed;
        public Stat<RangedStat> RangedAttackRangeStat => _rangedStats.AttackRange;
        public Stat<RangedStat> RangedDamageMultiplierStat => _rangedStats.DamageMultiplier;

        private readonly CharacterStats _characterStats;
        private MeleeStats _meleeStats;
        private RangedStats _rangedStats;

        public AllStats(InitialStats initialStats)
        {
            _characterStats = new CharacterStats(initialStats.Character);
            _meleeStats = new MeleeStats(initialStats.Melee);
            _rangedStats = new RangedStats(initialStats.Ranged);
        }
    }
}
