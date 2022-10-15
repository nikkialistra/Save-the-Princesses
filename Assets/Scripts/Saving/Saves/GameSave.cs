using Saving.Progress;

namespace Saving.Saves
{
    public class GameSave
    {
        public GameProgress Progress { get; }

        public GameSave()
        {
            Progress = new GameProgress();
        }
    }
}
