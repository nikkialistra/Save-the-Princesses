using Enemies;
using GameData.Enemies.Spawning.Frequencies;
using GameSystems;

namespace GameData.Enemies.Spawning
{
    public class EnemyPicking
    {
        private readonly GameModeControl _gameModeControl;

        public EnemyPicking(GameModeControl gameModeControl)
        {
            _gameModeControl = gameModeControl;
        }

        public EnemyType GetRandomEnemyType(EnemyRoomFrequencies enemyRoomFrequencies)
        {
            var frequencies = enemyRoomFrequencies[_gameModeControl.CurrentDifficulty];

            return frequencies.GetRandom();
        }
    }
}
