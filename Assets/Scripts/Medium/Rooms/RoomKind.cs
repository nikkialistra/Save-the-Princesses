using SuperTiled2Unity;
using UnityEngine;

namespace Medium.Rooms
{
    [CreateAssetMenu(fileName = "Room Kind", menuName = "Room Kind")]
    public class RoomKind : ScriptableObject
    {
        public SuperMap Map;
    }
}
