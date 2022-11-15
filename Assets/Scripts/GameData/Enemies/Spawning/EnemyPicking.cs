using Enemies;
using GameData.Enemies.Spawning.Frequencies;
using GameSystems;

namespace GameData.Enemies.Spawning
{
    public class EnemyPicking
    {
        private readonly GameControl _gameControl;

        public EnemyPicking(GameControl gameControl)
        {
            _gameControl = gameControl;
        }

        public EnemyType GetRandomEnemyType(EnemyRoomFrequencies enemyRoomFrequencies)
        {
            var frequencies = enemyRoomFrequencies[_gameControl.CurrentDifficulty];

            return frequencies.GetRandom();
        }
    }
}
