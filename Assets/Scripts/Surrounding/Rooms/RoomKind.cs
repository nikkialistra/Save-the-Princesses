using Data.Enemies.Spawning.Frequencies;
using Data.Princesses.Spawning.Frequencies;
using SuperTiled2Unity;
using UnityEngine;

namespace Surrounding.Rooms
{
    [CreateAssetMenu(fileName = "(Room Kind Name)", menuName = "Game Elements/Room Kind")]
    public class RoomKind : ScriptableObject
    {
        public PrincessCategoryRoomFrequencies PrincessCategoryRoomFrequencies;
        public EnemyRoomFrequencies EnemyRoomFrequencies;
        public SuperMap Map;
    }
}
