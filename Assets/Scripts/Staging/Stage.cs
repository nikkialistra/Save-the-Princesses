using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Rooms;
using Rooms.Services;
using Surrounding;
using UnityEngine;
using Zenject;

namespace Staging
{
    public class Stage : MonoBehaviour
    {
        public Room ActiveRoom { get; private set; }

        private readonly List<Room> _rooms = new();

        private RoomGenerator _roomGenerator;

        private ActiveRepositories _activeRepositories;

        private StageType _stageType;

        [Inject]
        public void Construct(RoomGenerator roomGenerator, ActiveRepositories activeRepositories)
        {
            _roomGenerator = roomGenerator;

            _activeRepositories = activeRepositories;
        }

        public async UniTask Initialize(StageType stageType)
        {
            name = stageType.GetName();

            _stageType = stageType;

            GenerateRooms();

            await SetupNavigation();

            GenerateCharacters();
        }

        private void GenerateRooms()
        {
            var room = _roomGenerator.Create(_stageType, transform);

            AddRoom(room);
            ChangeActiveRoomTo(room);
        }

        private async UniTask SetupNavigation()
        {
            await UniTask.NextFrame();

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
