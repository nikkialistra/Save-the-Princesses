using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Surrounding.Rooms.Services
{
    public class RoomFactory : SerializedMonoBehaviour
    {
        private DiContainer _diContainer;

        [Inject]
        public void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public Room Create(RoomKind roomKind, Transform parent)
        {
            var room = _diContainer.InstantiatePrefabForComponent<Room>(roomKind.Map);
            room.Initialize(roomKind, parent);

            return room;
        }
    }
}
