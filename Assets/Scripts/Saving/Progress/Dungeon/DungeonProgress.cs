using System;
using Saving.Progress.Dungeon.State;
using Saving.Progress.Dungeon.State.Rooms;

namespace Saving.Progress.Dungeon
{
    [Serializable]
    public class DungeonProgress
    {
        public HeroState HeroState;
        public TrainState TrainState;
        public RoomsState RoomsState;
        public WorldState WorldState;

        public DungeonProgress()
        {
            HeroState = new HeroState();
            TrainState = new TrainState();
            RoomsState = new RoomsState();
            WorldState = new WorldState();
        }
    }
}
