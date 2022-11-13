using System.Collections.Generic;
using Characters.Stats.Character;
using Characters.Stats.Melee;
using Characters.Stats.Ranged;
using StatSystem;
using UnityEngine;

namespace Characters.Traits
{
    [CreateAssetMenu(fileName = "(Trait Name)", menuName = "GameData/Character Trait")]
    public class Trait : ScriptableObject
    {
        public IReadOnlyCollection<StatModifier<CharacterStat>> CharacterStatModifiers => _characterStatModifiers;
        public IReadOnlyCollection<StatModifier<MeleeStat>> MeleeStatModifiers => _meleeStatModifiers;
        public IReadOnlyCollection<StatModifier<RangedStat>> RangedStatModifiers => _rangedStatModifiers;

        [SerializeField] private List<StatModifier<CharacterStat>> _characterStatModifiers;
        [SerializeField] private List<StatModifier<MeleeStat>> _meleeStatModifiers;
        [SerializeField] private List<StatModifier<RangedStat>> _rangedStatModifiers;
    }
}
