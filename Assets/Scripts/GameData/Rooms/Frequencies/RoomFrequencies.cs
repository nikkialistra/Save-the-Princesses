using System;
using GameData.Chances;
using Surrounding.Rooms;

namespace GameData.Rooms.Frequencies
{
    [Serializable]
    public class RoomFrequencies : ChanceList<RoomFrequency, RoomKind> { }
}
