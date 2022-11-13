using System.Collections.Generic;
using GameData.Stats;
using GameSystems;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameData.Enemies
{
    [CreateAssetMenu(fileName = "(Enemy Type)", menuName = "GameData/Stats/Enemy Initial Stats")]
    public class EnemyInitialStats : SerializedScriptableObject
    {
        public Dictionary<GameDifficulty, InitialStats> InitialStatsMap;
    }
}
