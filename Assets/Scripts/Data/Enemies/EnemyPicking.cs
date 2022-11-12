using Enemies;
using GameSystems;

namespace Data.Enemies
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
