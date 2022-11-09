using System.Collections.Generic;
using Surrounding.Rooms;
using UnityEngine;
using Zenject;

namespace Surrounding.Staging
{
    public class Stage : MonoBehaviour
    {
        public Room ActiveRoom { get; private set; }

        private readonly List<Room> _rooms = new();

        private RoomGenerator _roomGenerator;

        private ActiveRepositories _activeRepositories;

        [Inject]
        public void Construct(RoomGenerator roomGenerator, ActiveRepositories activeRepositories)
        {
            _roomGenerator = roomGenerator;

            _activeRepositories = activeRepositories;
        }

        public void Initialize()
        {
            var room = _roomGenerator.Create();
            AddRoom(room);
            ChangeActiveRoomTo(room);
        }

        public void Dispose()
        {
            foreach (var room in _rooms)
                room.Dispose();
        }

        private void AddRoom(Room room)
        {
            _rooms.Add(room);

            room.PlaceUnder(transform);
        }

        private void ChangeActiveRoomTo(Room room)
        {
            _activeRepositories.FillRepositories(room.Repositories);
            ActiveRoom = room;
        }

        public class Factory : PlaceholderFactory<Stage> { }
    }
}
