using System;
using GameData.Stats.Types;

namespace GameData.Stats
{
    [Serializable]
    public class InitialStats
    {
        public InitialCharacterStats Character = new();
        public InitialMeleeStats Melee = new();
        public InitialRangedStats Ranged = new();
    }
}
