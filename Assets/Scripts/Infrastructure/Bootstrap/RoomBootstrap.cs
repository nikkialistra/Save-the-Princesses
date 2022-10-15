using System;
using Surrounding.Rooms;
using Zenject;

namespace Infrastructure.Bootstrap
{
    public class RoomBootstrap : IInitializable, IDisposable
    {
        private readonly RoomRepositories _repositories;

        public RoomBootstrap(RoomRepositories repositories)
        {
            _repositories = repositories;
        }

        public void Initialize()
        {
            _repositories.Initialize();
        }

        public void Dispose()
        {
            _repositories.Dispose();
        }
    }
}
