using Characters.Stats;
using Characters.Stats.Character;
using Characters.Stats.Melee;
using Characters.Stats.Ranged;
using Heroes;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Stats
{
    public class StatsView : MonoBehaviour
    {
        private StatView _maxHealth;
        private StatView _meleeDamage;
        private StatView _rangedDamage;
        private StatView _meleeSpeed;
        private StatView _rangedSpeed;
        private StatView _movementSpeed;
        private StatView _armor;
        private StatView _vampirism;
        private StatView _luck;

        private VisualElement _root;

        public void Initialize(Hero hero)
        {
            _root = GetComponent<UIDocument>().rootVisualElement;

            var allStats = hero.Stats;

            BindStatsUi();
            BindStats(hero.Stats);
        }

        public void Dispose()
        {
            UnbindStats();
        }

        private void BindStats(AllStats stats)
        {
            _maxHealth.BindStat(stats.MaxHealthStat);

            _meleeDamage.BindStat(stats.MeleeDamageMultiplierStat);
            _meleeSpeed.BindStat(stats.MeleeAttackSpeedStat);

            _rangedDamage.BindStat(stats.RangedDamageMultiplierStat);
            _rangedSpeed.BindStat(stats.RangedAttackSpeedStat);

            _movementSpeed.BindStat(stats.MovementSpeedStat);
            _armor.BindStat(stats.ArmorStat);
            _vampirism.BindStat(stats.VampirismStat);
            _luck.BindStat(stats.LuckStat);
        }

        private void UnbindStats()
        {
            _maxHealth.UnbindStat();
            _meleeDamage.UnbindStat();
            _meleeSpeed.UnbindStat();
            _rangedDamage.UnbindStat();
            _rangedSpeed.UnbindStat();
            _movementSpeed.UnbindStat();
            _armor.UnbindStat();
            _vampirism.UnbindStat();
            _luck.UnbindStat();
        }

        private void BindStatsUi()
        {
            BindStatUi(out _maxHealth, "max-health");
            BindStatUi(out _meleeDamage, "melee-damage");
            BindStatUi(out _rangedDamage, "ranged-damage");
            BindStatUi(out _meleeSpeed, "melee-speed");
            BindStatUi(out _rangedSpeed, "ranged-speed");
            BindStatUi(out _movementSpeed, "movement-speed");
            BindStatUi(out _armor, "armor");
            BindStatUi(out _vampirism, "vampirism");
            BindStatUi(out _luck, "luck");

            _armor.HideWhenZero = true;
            _vampirism.HideWhenZero = true;
            _luck.HideWhenZero = true;
        }

        private void BindStatUi(out StatView statView, string name)
        {
            var container = _root.Q<VisualElement>($"{name}");
            var value = _root.Q<Label>($"{name}__value");
            var modifier = _root.Q<Label>($"{name}__modifier");

            statView = new StatView(container, value, modifier);
        }
    }
}
