using System;
using GameData.Chances;
using Rooms;

namespace GameData.Rooms.Frequencies
{
    [Serializable]
    public class RoomFrequencies : ChanceList<RoomFrequency, RoomKind> { }
}
