using System;
using Enemies;
using GameData.Chances;

namespace GameData.Enemies.Spawning.Frequencies
{
    [Serializable]
    public class EnemyFrequencies : ChanceList<EnemyFrequency, EnemyType> { }
}
