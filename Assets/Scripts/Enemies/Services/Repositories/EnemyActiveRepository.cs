using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Services.Repositories
{
    public class EnemyActiveRepository
    {
        public event Action<Enemy> Adding;
        public event Action<Enemy> Removing;

        public int Count => _roomRepository.Count;

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
            Adding?.Invoke(enemy);
        }

        public void Remove(Enemy enemy)
        {
            _roomRepository.Remove(enemy);
            Removing?.Invoke(enemy);
        }
    }
}
