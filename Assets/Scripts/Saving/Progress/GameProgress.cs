using System;
using Saving.Progress.Dungeon;
using Saving.Progress.State;

namespace Saving.Progress
{
    [Serializable]
    public class GameProgress
    {
        public GameState GameState;
        public HubProgress HubProgress;
        public DungeonProgress DungeonProgress;
    }
}
