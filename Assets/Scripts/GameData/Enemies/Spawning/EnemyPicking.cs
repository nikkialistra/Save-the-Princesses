using Enemies;
using GameData.Enemies.Spawning.Frequencies;
using GameSystems;

namespace GameData.Enemies.Spawning
{
    public class EnemyPicking
    {
        private readonly GameParameters _gameParameters;

        public EnemyPicking(GameParameters gameParameters)
        {
            _gameParameters = gameParameters;
        }

        public EnemyType GetRandomEnemyType(EnemyRoomFrequencies enemyRoomFrequencies)
        {
            var frequencies = enemyRoomFrequencies[_gameParameters.CurrentDifficulty];

            return frequencies.GetRandom();
        }
    }
}
