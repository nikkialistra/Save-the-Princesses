using Saving.Progress;
using Saving.Progress.Dungeon;

namespace Saving.Interfaces
{
    public interface ISavedProgressReader
    {
        void LoadProgress(DungeonProgress progress);
    }
}
