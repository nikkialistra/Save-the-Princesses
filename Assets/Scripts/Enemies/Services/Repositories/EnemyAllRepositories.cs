using System;
using System.Collections.Generic;

namespace Enemies.Services.Repositories
{
    public class EnemyAllRepositories
    {
        private readonly List<EnemyRoomRepository> _roomRepositories = new();

        public void Add(EnemyRoomRepository roomRepository)
        {
            if (_roomRepositories.Contains(roomRepository))
                throw new InvalidOperationException("Cannot add already contained enemy room repository");

            _roomRepositories.Add(roomRepository);
        }

        public void Remove(EnemyRoomRepository roomRepository)
        {
            if (!_roomRepositories.Contains(roomRepository))
                throw new InvalidOperationException("Cannot remove not contained enemy room repository");

            roomRepository.Dispose();

            _roomRepositories.Remove(roomRepository);
        }
    }
}
