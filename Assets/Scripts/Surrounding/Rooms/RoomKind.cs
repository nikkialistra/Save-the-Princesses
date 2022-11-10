using SuperTiled2Unity;
using Surrounding.Staging;
using UnityEngine;

namespace Surrounding.Rooms
{
    [CreateAssetMenu(fileName = "(Room Kind Name)", menuName = "Game Elements/Room Kind")]
    public class RoomKind : ScriptableObject
    {
        public StageType StageType;
        public SuperMap Map;
    }
}
