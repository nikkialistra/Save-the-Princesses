using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameData.Stats.Types
{
    [Serializable]
    public class InitialCharacterStats
    {
        [MinValue(0)]
        public float MaxHealth;

        [MinValue(0)]
        public float MovementSpeed;

        [Range(0, 1)]
        public float Armor;

        [Range(0, 1)]
        public float Vampirism;

        [Range(0, 1)]
        public float Luck;

        [MinValue(0)]
        public float KnockbackStand;

        [MinValue(0)]
        public float StunResistance;
    }
}
