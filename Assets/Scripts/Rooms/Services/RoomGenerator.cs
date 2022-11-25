using Staging;
using UnityEngine;

namespace Rooms.Services
{
    public class RoomGenerator
    {
        private readonly RoomPicking _picking;

        private readonly RoomFactory _factory;

        public RoomGenerator(RoomPicking picking, RoomFactory factory)
        {
            _picking = picking;
            _factory = factory;
        }

        public Room Create(StageType stageType, Transform parent)
        {
            var roomKind = _picking.GetRandomFor(stageType);

            var room = _factory.Create(roomKind, parent);

            return room;
        }
    }
}
