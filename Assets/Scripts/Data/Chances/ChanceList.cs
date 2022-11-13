using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Data.Chances
{
    [Serializable]
    public class ChanceList<TElementsSetup, TValue> where TElementsSetup : ChanceSetup<TValue>, new()
    {
        public bool Empty => _elements.Count == 0;

        [SerializeField] private List<TElementsSetup> _elements = new();

        public TValue GetRandom()
        {
            if (Empty)
                throw new InvalidOperationException("Cannot get random element from empty chance list");

            var randomValue = Random.Range(0f, 1f);

            foreach (var element in _elements)
            {
                if (randomValue <= element.Chance)
                    return element.Element;

                randomValue -= element.Chance;
            }

            throw new InvalidOperationException("Chances sum were not one");
        }

        [HorizontalGroup("Split", 0.5f)]
        [Button(ButtonSizes.Medium), GUIColor(0.4f, 0.8f, 1)]
        private void AddNewElement()
        {
            _elements.Add(new TElementsSetup());

            RecalculateChances();
        }

        [HorizontalGroup("Split")]
        [Button(ButtonSizes.Medium), GUIColor(0, 1, 0)]
        private void RecalculateChances()
        {
            if (Empty) return;

            var relativeChancesSum = _elements.Sum(element => element.RelativeChance);

            foreach (var element in _elements)
                element.Chance = (float)element.RelativeChance / relativeChancesSum;
        }
    }
}
