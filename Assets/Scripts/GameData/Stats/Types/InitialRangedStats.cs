using System;
using Sirenix.OdinInspector;

namespace GameData.Stats.Types
{
    [Serializable]
    public class InitialRangedStats
    {
        [MinValue(0)]
        public float AttackSpeed = 1;

        [MinValue(0)]
        public float AttackRange = 1;

        [MinValue(0)]
        public float DamageMultiplier = 1;

        [MinValue(0)]
        public float KnockbackMultiplier = 1;

        [MinValue(0)]
        public float StunMultiplier = 1;
    }
}
