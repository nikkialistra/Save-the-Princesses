using System.Collections.Generic;
using Enemies;
using UnityEngine;

namespace Rooms.Services.RepositoryTypes.Enemies
{
    public class EnemyActiveRepository
    {
        public IEnumerable<Enemy> Enemies => _roomRepository.Enemies;

        private EnemyRoomRepository _roomRepository;

        public void Initialize(EnemyRoomRepository initialRepository)
        {
            _roomRepository = initialRepository;
        }

        public void Dispose()
        {
            _roomRepository.Dispose();
        }

        public void ReplaceRoomRepository(EnemyRoomRepository newRepository)
        {
            _roomRepository = newRepository;
        }

        public void Add(Enemy enemy, Vector3 position)
        {
            _roomRepository.Add(enemy, position);
        }

        public void Remove(Enemy enemy)
        {
            _roomRepository.Remove(enemy);
        }
    }
}
