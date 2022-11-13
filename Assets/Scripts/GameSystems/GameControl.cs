namespace GameSystems
{
    public class GameControl
    {
        public GameDifficulty CurrentDifficulty { get; private set; } = GameDifficulty.Training;

        public void ChangeGameToNormalMode()
        {
            CurrentDifficulty = GameDifficulty.Normal;
        }
    }
}
