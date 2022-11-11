using Enemies;
using Sirenix.OdinInspector;
using Surrounding.Staging;
using UnityEngine;

namespace Data.Enemies
{
    public class EnemyFrequenciesRegistry : SerializedMonoBehaviour
    {
        [SerializeField] private EnemyStageFrequencies _enemyStageFrequencies;

        public EnemyType GetRandomEnemyTypeFor(StageType stageType)
        {
            var frequencies = _enemyStageFrequencies.Frequencies[stageType];

            return frequencies.GetRandom();
        }
    }
}
