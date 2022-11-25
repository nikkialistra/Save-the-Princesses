using GameData.Enemies.Spawning;
using GameData.Enemies.Spawning.Frequencies;
using Rooms.Services.RepositoryTypes.Enemies;
using SuperTiled2Unity;
using UnityEngine;

namespace Enemies.Services
{
    public class EnemyGenerator
    {
        private Transform[] _spawnPoints;

        private readonly EnemyPicking _picking;

        private readonly EnemyFactory _factory;

        private readonly EnemyRoomRepository _repository;

        public EnemyGenerator(EnemyPicking picking, EnemyFactory factory, EnemyRoomRepository repository)
        {
            _picking = picking;
            _factory = factory;
            _repository = repository;
        }

        public void Initialize(SuperObjectLayer spawnPointsLayer)
        {
            _spawnPoints = spawnPointsLayer.GetComponentsInChildren<Transform>();
        }

        public void Generate(EnemyRoomFrequencies roomFrequencies)
        {
            foreach (var spawnPoint in _spawnPoints)
                SpawnFor(spawnPoint.position, roomFrequencies);
        }

        private void SpawnFor(Vector3 position, EnemyRoomFrequencies roomFrequencies)
        {
            var enemyType = _picking.GetRandomEnemyType(roomFrequencies);

            if (enemyType == EnemyType.None) return;

            var enemy = _factory.Create(enemyType);

            _repository.Add(enemy, position);
        }
    }
}
