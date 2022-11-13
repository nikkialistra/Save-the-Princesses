using Data.Enemies.Spawning.Frequencies;
using Enemies;
using GameSystems;

namespace Data.Enemies.Spawning
{
    public class EnemyPicking
    {
        private readonly GameControl _gameControl;

        public  EnemyPicking(GameControl gameControl)
        {
            _gameControl = gameControl;
        }

        public EnemyType GetRandomEnemyType(EnemyRoomFrequencies enemyRoomFrequencies)
        {
            var frequencies = enemyRoomFrequencies.Frequencies[_gameControl.CurrentDifficulty];

            return frequencies.GetRandom();
        }
    }
}
