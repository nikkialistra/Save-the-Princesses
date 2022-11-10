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

        private readonly EnemyFactory _enemyFactory;

        private readonly EnemyRoomRepository _repository;

        public EnemyGenerator(EnemyFactory enemyFactory, EnemyRoomRepository repository)
        {
            _enemyFactory = enemyFactory;
            _repository = repository;
        }

        public void Initialize(SuperObjectLayer spawnPointsLayer)
        {
            var spawnPoints = spawnPointsLayer.GetComponentsInChildren<SuperObject>();
            _spawnPoints = new SpawnPoints(spawnPoints);
        }

        public void Generate()
        {
            var count = ComputeCount();

            foreach (var spawnPoint in _spawnPoints.TakeSome(count))
                SpawnAt(EnemyType.Apostate, spawnPoint);
        }

        private static int ComputeCount()
        {
            return Random.Range(0, 4);
        }

        private void SpawnAt(EnemyType enemyType, Vector3 position)
        {
            var enemy = _enemyFactory.Create(enemyType);
            enemy.Initialize();

            _repository.Add(enemy, position);
        }
    }
}
