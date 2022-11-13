using System;
using Sirenix.OdinInspector;

namespace GameData.Stats.Types
{
    [Serializable]
    public class InitialRangedStats
    {
        [MinValue(0)]
        public float AttackSpeed;

        [MinValue(0)]
        public float AttackRange;

        [MinValue(1)]
        public float DamageMultiplier;
    }
}
