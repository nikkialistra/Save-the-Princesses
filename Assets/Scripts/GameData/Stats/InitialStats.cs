using System;
using GameData.Stats.Types;
using UnityEngine;

namespace GameData.Stats
{
    [Serializable]
    public class InitialStats : ScriptableObject
    {
        public InitialCharacterStats Character;
        public InitialMeleeStats Melee;
        public InitialRangedStats Ranged;
    }
}
