using System;
using System.Collections.Generic;
using System.Linq;
using Enemies;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Data.Enemies
{
    [CreateAssetMenu(fileName = "(Stage Type)", menuName = "Data/Stage Enemy Frequencies")]
    public class StageEnemyFrequencies : SerializedScriptableObject
    {
        public Dictionary<EnemyType, EnemyFrequency> Map;

        public EnemyType GetRandom()
        {
            var randomValue = Random.Range(0f, 1f);

            foreach (var (key, value) in Map)
            {
                if (randomValue <= value.Chance)
                    return key;

                randomValue -= value.Chance;
            }

            throw new InvalidOperationException("Chances sum were not one");
        }

        [HorizontalGroup("Split", 0.5f)]
        [Button(ButtonSizes.Medium), GUIColor(0.4f, 0.8f, 1)]
        private void AddNewElement()
        {
            Map.Add(EnemyType.Apostate, new EnemyFrequency());

            RecalculateChances();
        }

        [HorizontalGroup("Split")]
        [Button(ButtonSizes.Medium), GUIColor(0, 1, 0)]
        private void RecalculateChances()
        {
            if (Map.Count == 0) return;

            var relativeChancesSum = Map.Values.Sum(enemyFrequency => enemyFrequency.RelativeChance);

            foreach (var enemyFrequency in Map.Values)
                enemyFrequency.Chance = (float)enemyFrequency.RelativeChance / relativeChancesSum;
        }
    }
}
