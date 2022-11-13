using System.Collections.Generic;
using GameData.Stats;
using Sirenix.OdinInspector;
using Surrounding.Staging;
using UnityEngine;

namespace GameData.Princesses
{
    [CreateAssetMenu(fileName = "(Princess Type)", menuName = "GameData/Stats/Princess Initial Stats")]
    public class PrincessInitialStats : SerializedScriptableObject
    {
        public Dictionary<StageType, InitialStats> InitialStatsMap = new();

        public InitialStats For(StageType stageType)
        {
            return InitialStatsMap[stageType];
        }
    }
}
