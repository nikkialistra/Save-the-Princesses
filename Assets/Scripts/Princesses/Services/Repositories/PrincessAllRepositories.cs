using System;
using System.Collections.Generic;

namespace Princesses.Services.Repositories
{
    public class PrincessAllRepositories
    {
        private readonly List<PrincessRoomRepository> _roomRepositories = new();

        public void Add(PrincessRoomRepository roomRepository)
        {
            if (_roomRepositories.Contains(roomRepository))
                throw new InvalidOperationException("Cannot add already contained princess room repository");

            _roomRepositories.Add(roomRepository);
        }

        public void Remove(PrincessRoomRepository roomRepository)
        {
            if (!_roomRepositories.Contains(roomRepository))
                throw new InvalidOperationException("Cannot remove not contained princess room repository");

            roomRepository.Dispose();

            _roomRepositories.Remove(roomRepository);
        }
    }
}
