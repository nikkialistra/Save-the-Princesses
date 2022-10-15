using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Medium.Rooms
{
    public class RoomGenerator : MonoBehaviour
    {
        [SerializeField] private List<RoomKind> _roomKinds;

        private List<Room> _rooms;

        private Room.Factory _roomFactory;

        [Inject]
        public void Construct(Room.Factory roomFactory)
        {
            _roomFactory = roomFactory;
        }

        public Room CreateFirstRoom()
        {
            var room = _roomFactory.Create();
            room.Initialize(_roomKinds[0]);

            _rooms.Add(room);

            return room;
        }

        public void Dispose()
        {
            foreach (var room in _rooms)
                room.Dispose();

            _rooms.Clear();
        }
    }
}
