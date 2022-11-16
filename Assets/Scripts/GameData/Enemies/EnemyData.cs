using System;
using Enemies;

namespace GameData.Enemies
{
    [Serializable]
    public class EnemyData
    {
        public Enemy Prefab;
        public EnemyInitialStats InitialStats;
        public EnemySpecs Specs;
    }
}
