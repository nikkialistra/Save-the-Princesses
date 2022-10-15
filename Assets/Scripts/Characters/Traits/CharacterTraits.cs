using System.Collections.Generic;
using Characters.Stats.Character;
using Characters.Stats.Melee;
using Characters.Stats.Ranged;
using StatSystem;
using UnityEngine;

namespace Characters.Traits
{
    public class CharacterTraits : MonoBehaviour
    {
        private CharacterStats _characterStats;
        private MeleeStats _meleeStats;
        private RangedStats _rangedStats;

        private readonly List<Trait> _traits = new();

        public void Initialize()
        {
            _characterStats = GetComponent<CharacterStats>();
            _meleeStats = GetComponent<MeleeStats>();
            _rangedStats = GetComponent<RangedStats>();
        }

        public void Dispose()
        {
            RemoveAllTraits();
        }

        public void Add(Trait trait)
        {
            _traits.Add(trait);

            if (_characterStats != null)
                Add(trait.CharacterStatModifiers);

            if (_meleeStats != null)
                Add(trait.MeleeStatModifiers);

            if (_rangedStats != null)
                Add(trait.RangedStatModifiers);
        }

        public void Remove(Trait trait)
        {
            if (!_traits.Contains(trait)) return;

            if (_characterStats != null)
                Remove(trait.CharacterStatModifiers);

            if (_meleeStats != null)
                Remove(trait.MeleeStatModifiers);

            if (_rangedStats != null)
                Remove(trait.RangedStatModifiers);

            _traits.Remove(trait);
        }

        private void RemoveAllTraits()
        {
            _traits.Clear();
        }

        private void Add(IEnumerable<StatModifier<CharacterStat>> statModifiers)
        {
            foreach (var statModifier in statModifiers)
                _characterStats.AddStatModifier(statModifier);
        }

        private void Add(IEnumerable<StatModifier<MeleeStat>> statModifiers)
        {
            foreach (var statModifier in statModifiers)
                _meleeStats.AddStatModifier(statModifier);
        }

        private void Add(IEnumerable<StatModifier<RangedStat>> statModifiers)
        {
            foreach (var statModifier in statModifiers)
                _rangedStats.AddStatModifier(statModifier);
        }

        private void Remove(IEnumerable<StatModifier<CharacterStat>> statModifiers)
        {
            foreach (var statModifier in statModifiers)
                _characterStats.RemoveStatModifier(statModifier);
        }

        private void Remove(IEnumerable<StatModifier<MeleeStat>> statModifiers)
        {
            foreach (var statModifier in statModifiers)
                _meleeStats.RemoveStatModifier(statModifier);
        }

        private void Remove(IEnumerable<StatModifier<RangedStat>> statModifiers)
        {
            foreach (var statModifier in statModifiers)
                _rangedStats.RemoveStatModifier(statModifier);
        }
    }
}
