using GameSystems.Parameters;

namespace GameSystems
{
    public class GameParameters
    {
        public GameDifficulty CurrentDifficulty { get; private set; } = GameDifficulty.Training;
        public GameMode GameMode { get; private set; } = GameMode.Single;

        public void ChangeGameDifficulty(GameDifficulty gameDifficulty)
        {
            CurrentDifficulty = gameDifficulty;
        }

        public void ChangeGameMode(GameMode gameMode)
        {
            GameMode = gameMode;
        }
    }
}
