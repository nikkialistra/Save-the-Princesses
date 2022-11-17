using System.Collections.Generic;
using GameData.Rooms.Frequencies;
using Surrounding.Rooms;
using Surrounding.Staging;
using UnityEngine;

namespace GameData.Rooms
{
    public class RoomFrequencyRegistry : MonoBehaviour
    {
        [SerializeField] private Dictionary<StageType, RoomStageFrequencies> _roomStageFrequenciesMap = new();

        public RoomKind GetRandomFor(StageType stageType)
        {
            var roomStageFrequencies = _roomStageFrequenciesMap[stageType].Frequencies;

            return roomStageFrequencies.GetRandom();
        }
    }
}
