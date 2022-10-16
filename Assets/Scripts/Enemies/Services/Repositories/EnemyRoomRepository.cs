using System;
using System.Collections.Generic;
using Entities.Containers;
using Surrounding.Rooms;
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

        public void Add(Enemy enemy)
        {
            _container.Add(enemy);
            Adding?.Invoke(enemy);
        }

        public void Remove(Enemy enemy)
        {
            _container.Remove(enemy);
            Removing?.Invoke(enemy);
        }
    }
}
