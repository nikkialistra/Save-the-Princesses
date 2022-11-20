namespace GameSystems
{
    public class GameModeControl
    {
        public GameDifficulty CurrentDifficulty { get; private set; } = GameDifficulty.Training;

        public void ChangeGameToNormalMode()
        {
            CurrentDifficulty = GameDifficulty.Normal;
        }
    }
}
