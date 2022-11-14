using Saving.Progress.Dungeon;

namespace Saving.Interfaces
{
    public interface ISavedProgress : ISavedProgressReader
    {
        void UpdateProgress(DungeonProgress progress);
    }
}
