using System.Collections.Generic;
using GameSystems;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameData.Enemies.Spawning.Frequencies
{
    [CreateAssetMenu(fileName = "(Room Type)", menuName = "GameData/Enemy Room Frequencies")]
    public class EnemyRoomFrequencies : SerializedScriptableObject
    {
        public Dictionary<GameDifficulty, EnemyFrequencies> Frequencies = new();

        public EnemyFrequencies this[GameDifficulty gameDifficulty] => Frequencies[gameDifficulty];
    }
}
