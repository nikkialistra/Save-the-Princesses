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

        public void Initialize(StageType stageType)
        {
            name = stageType.GetName();

            GenerateRooms();
            SetupNavigation();

            GenerateCharacters();
        }

        private void GenerateRooms()
        {
            var room = _roomGenerator.Create(transform);

            AddRoom(room);
            ChangeActiveRoomTo(room);
        }

        private void SetupNavigation()
        {
            foreach (var room in _rooms)
                room.SetupNavigation();
        }

        private void GenerateCharacters()
        {
            foreach (var room in _rooms)
                room.GenerateCharacters();
        }

        public void Dispose()
        {
            foreach (var room in _rooms)
                room.Dispose();
        }

        private void AddRoom(Room room)
        {
            _rooms.Add(room);
        }

        private void ChangeActiveRoomTo(Room room)
        {
            _activeRepositories.FillRepositories(room.Repositories);
            ActiveRoom = room;
        }

        public class Factory : PlaceholderFactory<Stage> { }
    }
}
