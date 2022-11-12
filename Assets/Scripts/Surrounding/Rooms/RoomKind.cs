using Data.Enemies;
using SuperTiled2Unity;
using UnityEngine;

namespace Surrounding.Rooms
{
    [CreateAssetMenu(fileName = "(Room Kind Name)", menuName = "Game Elements/Room Kind")]
    public class RoomKind : ScriptableObject
    {
        public EnemyRoomFrequencies EnemyRoomFrequencies;
        public SuperMap Map;
    }
}
