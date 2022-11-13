using System;
using Data.Chances;
using Enemies;

namespace Data.Enemies.Spawning.Frequencies
{
    [Serializable]
    public class EnemyFrequencies : ChanceList<EnemyFrequency, EnemyType> { }
}
