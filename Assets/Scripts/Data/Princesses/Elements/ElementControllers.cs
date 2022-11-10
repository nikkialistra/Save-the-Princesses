using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Data.Princesses.Elements
{
    [Serializable]
    public class ElementControllers
    {
        public bool Empty => _elementControllers.Count == 0;

        [SerializeField] private List<ElementController> _elementControllers = new();

        public RuntimeAnimatorController GetRandom()
        {
            if (Empty)
                throw new InvalidOperationException("Cannot get random element from empty controllers list");

            var randomValue = Random.Range(0f, 1f);

            foreach (var elementController in _elementControllers)
            {
                if (randomValue <= elementController.Chance)
                    return elementController.Controller;

                randomValue -= elementController.Chance;
            }

            throw new InvalidOperationException("Chances sum were not one");
        }

        [HorizontalGroup("Split", 0.5f)]
        [Button(ButtonSizes.Medium), GUIColor(0.4f, 0.8f, 1)]
        private void AddNewElement()
        {
            _elementControllers.Add(new ElementController());

            RecalculateChances();
        }

        [HorizontalGroup("Split")]
        [Button(ButtonSizes.Medium), GUIColor(0, 1, 0)]
        private void RecalculateChances()
        {
            if (Empty) return;

            var relativeChancesSum = _elementControllers.Sum(elementController => elementController.RelativeChance);

            foreach (var elementController in _elementControllers)
                elementController.Chance = (float)elementController.RelativeChance / relativeChancesSum;
        }
    }
}
