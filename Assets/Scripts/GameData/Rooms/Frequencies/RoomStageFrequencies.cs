using Sirenix.OdinInspector;
using UnityEngine;

namespace GameData.Rooms.Frequencies
{
    [CreateAssetMenu(fileName = "(Stage Type)", menuName = "GameData/Frequencies/Room Stage Frequencies")]
    public class RoomStageFrequencies : SerializedScriptableObject
    {
        public RoomFrequencies Frequencies = new();
    }
}
