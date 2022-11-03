using Enemies.Services.Repositories;
using SuperTiled2Unity;
using Surrounding;
using Surrounding.Rooms;
using UnityEngine;

namespace Enemies.Services
{
    public class EnemyGenerator
    {
        private SpawnPoints _spawnPoints;

        private readonly Enemy.Factory _enemyFactory;

        private readonly EnemyRoomRepository _repository;

        public EnemyGenerator(Enemy.Factory enemyFactory, EnemyRoomRepository repository)
        {
            _enemyFactory = enemyFactory;
            _repository = repository;
        }

        public void Initialize(SuperObjectLayer spawnPointsLayer)
        {
            var spawnPoints = spawnPointsLayer.GetComponents<SuperObject>();
            _spawnPoints = new SpawnPoints(spawnPoints);
        }

        public void Generate()
        {
            var count = ComputeCount();

            foreach (var spawnPoint in _spawnPoints.TakeSome(count))
                SpawnAt(spawnPoint);
        }

        private static int ComputeCount()
        {
            return Random.Range(0, 4);
        }

        private void SpawnAt(Vector3 position)
        {
            var enemy = _enemyFactory.Create();

            _repository.Add(enemy, position);
        }
    }
}
