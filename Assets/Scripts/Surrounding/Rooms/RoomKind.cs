using GameData.Enemies.Spawning.Frequencies;
using GameData.Princesses.Spawning.Frequencies;
using SuperTiled2Unity;
using UnityEngine;

namespace Surrounding.Rooms
{
    [CreateAssetMenu(fileName = "(Room Kind Name)", menuName = "GameData/Room Kind")]
    public class RoomKind : ScriptableObject
    {
        public PrincessCategoryRoomFrequencies PrincessCategoryRoomFrequencies;
        public EnemyRoomFrequencies EnemyRoomFrequencies;
        public SuperMap Map;
    }
}
