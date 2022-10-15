﻿using Enemies.Services.Repositories;
using Princesses.Services.Repositories;

namespace Medium.Rooms
{
    public class RoomRepositories
    {
        public PrincessRoomRepository Princesses => _princessRepository;
        public EnemyRoomRepository Enemies => _enemyRepository;

        private readonly PrincessRoomRepository _princessRepository;
        private readonly EnemyRoomRepository _enemyRepository;

        public RoomRepositories(PrincessRoomRepository princessRepository, EnemyRoomRepository enemyRepository)
        {
            _princessRepository = princessRepository;
            _enemyRepository = enemyRepository;
        }

        public void Initialize()
        {
            _princessRepository.Initialize();
            _enemyRepository.Initialize();
        }

        public void Dispose()
        {
            _princessRepository.Dispose();
            _enemyRepository.Dispose();
        }
    }
}
