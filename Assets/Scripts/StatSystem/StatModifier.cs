using System;
using UnityEngine;

namespace StatSystem
{
    [Serializable]
    public class StatModifier<T>
    {
        public T StatType => _statType;
        public StatModifierType ModifierType => _modifierType;

        public float Value => _value;

        public int Order => (int)_modifierType;

        [SerializeField] private T _statType;
        [SerializeField] private StatModifierType _modifierType;

        [Space]
        [SerializeField] private float _value;

        public StatModifier(T statType, StatModifierType modifierType, float value)
        {
            _statType = statType;
            _modifierType = modifierType;

            _value = value;
        }
    }
}
