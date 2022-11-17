using GameData.Rooms;
using Surrounding.Staging;

namespace Surrounding.Rooms.Services
{
    public class RoomPicking
    {
        private readonly RoomFrequencyRegistry _roomFrequencyRegistry;

        public RoomPicking(RoomFrequencyRegistry roomFrequencyRegistry)
        {
            _roomFrequencyRegistry = roomFrequencyRegistry;
        }

        public RoomKind GetRandomFor(StageType stageType)
        {
            return _roomFrequencyRegistry.GetRandomFor(stageType);
        }
    }
}
