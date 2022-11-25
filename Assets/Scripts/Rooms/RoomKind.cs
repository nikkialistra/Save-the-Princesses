using GameData.Enemies.Spawning.Frequencies;
using GameData.Princesses.Spawning.Frequencies;
using Staging;
using SuperTiled2Unity;
using UnityEngine;

namespace Rooms
{
    [CreateAssetMenu(fileName = "(Room Kind Name)", menuName = "GameData/Room Kind")]
    public class RoomKind : ScriptableObject
    {
        public StageType StageType;
        public SuperMap Map;
        public PrincessCategoryRoomFrequencies PrincessCategoryRoomFrequencies;
        public EnemyRoomFrequencies EnemyRoomFrequencies;
    }
}
