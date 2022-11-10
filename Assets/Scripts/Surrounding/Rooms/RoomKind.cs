using SuperTiled2Unity;
using Surrounding.Staging;
using UnityEngine;

namespace Surrounding.Rooms
{
    [CreateAssetMenu(fileName = "Room Kind", menuName = "Room Kind")]
    public class RoomKind : ScriptableObject
    {
        public StageType StageType;
        public SuperMap Map;
    }
}
