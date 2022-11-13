using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace StatSystem
{
    [Serializable]
    public class Stat<T> : IStat
    {
        public event Action<float> ValueChange;

        public float Value { get; private set; }
        public float BaseValue => _baseValue;

        [ReadOnly]
        [SerializeField] private float _baseValue;

        private readonly List<StatModifier<T>> _statModifiers = new();

        public Stat(float baseValue)
        {
            _baseValue = baseValue;
            RecalculateValue();
        }

        public void ChangeBaseValue(float baseValue)
        {
            _baseValue = baseValue;
            RecalculateValue();
        }

        public void AddModifier(StatModifier<T> modifier)
        {
            _statModifiers.Add(modifier);
            RecalculateValue();
        }

        public bool RemoveModifier(StatModifier<T> modifier)
        {
            if (_statModifiers.Remove(modifier))
            {
                RecalculateValue();
                return true;
            }

            return false;
        }

        public bool RemoveAllModifiersFromSource(object source)
        {
            var removedCount = _statModifiers.RemoveAll(modifier => modifier.Source == source);

            if (removedCount > 0)
            {
                RecalculateValue();
                return true;
            }

            return false;
        }

        private void RecalculateValue()
        {
            var finalValue = _baseValue;
            var percentAddSum = 0f;

            _statModifiers.Sort(CompareModifierOrder);

            for (int i = 0; i < _statModifiers.Count; i++)
            {
                var modifier = _statModifiers[i];

                switch (modifier.ModifierType)
                {
                    case StatModifierType.EarlyFlat:
                        finalValue += modifier.Value;
                        break;
                    case StatModifierType.PercentAdd:
                    {
                        percentAddSum += modifier.Value;

                        if (IsPercentAddModifiersEnd(i))
                            finalValue *= 1 + percentAddSum;

                        break;
                    }
                    case StatModifierType.PercentMultiply:
                        finalValue *= modifier.Value;
                        break;
                    case StatModifierType.LateFlat:
                        finalValue += modifier.Value;
                        break;
                }
            }

            // Workaround for float calculation errors, like displaying 12.00001 instead of 12
            Value = (float)Math.Round(finalValue, 4);

            ValueChange?.Invoke(Value);
        }

        private int CompareModifierOrder(StatModifier<T> a, StatModifier<T> b)
        {
            if (a.Order < b.Order)
                return -1;

            if (a.Order > b.Order)
                return 1;

            return 0;
        }

        private bool IsPercentAddModifiersEnd(int i)
        {
            return i + 1 >= _statModifiers.Count || _statModifiers[i + 1].ModifierType != StatModifierType.PercentAdd;
        }
    }
}
