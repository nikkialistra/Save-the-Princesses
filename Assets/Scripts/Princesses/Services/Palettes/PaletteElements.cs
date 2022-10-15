using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Princesses.Services.Palettes
{
    [Serializable]
    public class PaletteElements<T> where T : ScriptableObject
    {
        public bool Empty => _paletteElements.Count == 0;

        [SerializeField] private List<PaletteElement<T>> _paletteElements = new();

        [HorizontalGroup("Split", 0.5f)]
        [Button(ButtonSizes.Medium), GUIColor(0.4f, 0.8f, 1)]
        private void AddNewElement()
        {
            _paletteElements.Add(new PaletteElement<T>());

            RecalculateChances();
        }

        [HorizontalGroup("Split")]
        [Button(ButtonSizes.Medium), GUIColor(0, 1, 0)]
        private void RecalculateChances()
        {
            if (Empty) return;

            var relativeChancesSum = _paletteElements.Sum(elementController => elementController.RelativeChance);

            foreach (var paletteElement in _paletteElements)
                paletteElement.Chance = paletteElement.RelativeChance / relativeChancesSum;
        }

        public T GetRandom()
        {
            if (Empty)
                throw new InvalidOperationException("Cannot get random element from empty palettes list");

            var randomValue = Random.Range(0f, 1f);

            foreach (var paletteElement in _paletteElements)
            {
                if (randomValue <= paletteElement.Chance)
                    return paletteElement.Palette;

                randomValue -= paletteElement.Chance;
            }

            throw new InvalidOperationException("Chances sum were not one");
        }
    }
}
