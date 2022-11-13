using GameData.Stats;
using UnityEngine;

namespace GameData.Heroes
{
    [CreateAssetMenu(fileName = "Stats", menuName = "GameData/Stats/Hero Initial Stats")]
    public class HeroInitialStats : ScriptableObject
    {
        public InitialStats InitialStats = new();
    }
}
