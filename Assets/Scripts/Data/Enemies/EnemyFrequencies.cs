using System.Collections.Generic;
using Enemies;
using Sirenix.OdinInspector;
using Surrounding.Staging;
using UnityEngine;

namespace Data.Enemies
{
    public class EnemyFrequencies : SerializedMonoBehaviour
    {
        [SerializeField] private Dictionary<StageType, StageEnemyFrequencies> _stageFrequencies;

        public EnemyType GetRandomEnemyTypeFor(StageType stageType)
        {
            var stageEnemyFrequencies = _stageFrequencies[stageType];

            return stageEnemyFrequencies.GetRandom();
        }
    }
}
