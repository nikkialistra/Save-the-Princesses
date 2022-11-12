using System.Collections.Generic;
using GameSystems;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data.Enemies
{
    [CreateAssetMenu(fileName = "(Room Type)", menuName = "Data/Enemy Room Frequencies")]
    public class EnemyRoomFrequencies : SerializedScriptableObject
    {
        public Dictionary<GameDifficulty, EnemyFrequencies> Frequencies = new();
    }
}
