using GameData.Enemies.Spawning.Frequencies;
using GameData.Princesses.Spawning.Frequencies;
using SuperTiled2Unity;
using Surrounding.Staging;
using UnityEngine;

namespace Surrounding.Rooms
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
