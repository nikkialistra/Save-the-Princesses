using System;
using System.Collections.Generic;
using Entities.Containers;
using Surrounding.Rooms;
using UnityEngine;
using Zenject;

namespace Enemies.Services.Repositories
{
    public class EnemyRoomRepository
    {
        public event Action<Enemy> Adding;
        public event Action<Enemy> Removing;

        public int Count => _container.Count;

        public IEnumerable<Enemy> Enemies => _container.Entities;

        private EnemyContainer _container;

        [Inject]
        public void Construct(EnemyContainer container)
        {
            _container = container;
        }

        public void Initialize(Room room)
        {
            _container.Initialize(room);
        }

        public void Dispose()
        {
            _container.Dispose();
        }

        public void Add(Enemy enemy, Vector3 position)
        {
            _container.Add(enemy, position);
            Adding?.Invoke(enemy);
        }

        public void Remove(Enemy enemy)
        {
            _container.Remove(enemy);
            Removing?.Invoke(enemy);
        }
    }
}
