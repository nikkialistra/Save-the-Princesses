using Enemies.Services.Repositories;
using SuperTiled2Unity;
using Surrounding.Rooms;
using UnityEngine;

namespace Enemies.Services
{
    public class EnemyGenerator
    {
        private readonly Enemy.Factory _enemyFactory;
        private SpawnPoints _spawnPoints;

        private readonly EnemyActiveRepository _activeRepository;

        public EnemyGenerator(Enemy.Factory enemyFactory, EnemyActiveRepository activeRepository)
        {
            _enemyFactory = enemyFactory;
            _activeRepository = activeRepository;
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

            _activeRepository.Add(enemy, position);
        }
    }
}
