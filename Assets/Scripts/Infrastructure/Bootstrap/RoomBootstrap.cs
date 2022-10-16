using System;
using Surrounding.Rooms;
using Zenject;

namespace Infrastructure.Bootstrap
{
    public class RoomBootstrap : IInitializable, IDisposable
    {
        private readonly Room _room;

        private readonly RoomRepositories _repositories;

        public RoomBootstrap(Room room, RoomRepositories repositories)
        {
            _room = room;
            _repositories = repositories;
        }

        public void Initialize()
        {
            _repositories.Initialize(_room);
        }

        public void Dispose()
        {
            _repositories.Dispose();
        }
    }
}
