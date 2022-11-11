using System.Collections.Generic;
using Sirenix.OdinInspector;
using Surrounding.Staging;
using UnityEngine;

namespace Data.Enemies
{
    [CreateAssetMenu(fileName = "Enemy Stage Frequencies", menuName = "Data/Enemy Stage Frequencies")]
    public class EnemyStageFrequencies : SerializedScriptableObject
    {
        public Dictionary<StageType, EnemyFrequencies> Frequencies;
    }
}
