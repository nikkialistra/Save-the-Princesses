using System;
using Sirenix.OdinInspector;

namespace GameData.Stats.Types
{
    [Serializable]
    public class InitialMeleeStats
    {
        [MinValue(0)]
        public float AttackSpeed;

        [MinValue(1)]
        public float DamageMultiplier;
    }
}
