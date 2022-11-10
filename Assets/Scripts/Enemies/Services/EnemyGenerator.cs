﻿using Data.Enemies;
using Enemies.Services.Repositories;
using SuperTiled2Unity;
using Surrounding.Rooms;
using Surrounding.Staging;
using UnityEngine;

namespace Enemies.Services
{
    public class EnemyGenerator
    {
        private SpawnPoints _spawnPoints;

        private readonly EnemyFrequencies _frequencies;

        private readonly EnemyFactory _factory;

        private readonly EnemyRoomRepository _repository;

        public EnemyGenerator(EnemyFrequencies frequencies, EnemyFactory factory, EnemyRoomRepository repository)
        {
            _frequencies = frequencies;
            _factory = factory;
            _repository = repository;
        }

        public void Initialize(SuperObjectLayer spawnPointsLayer)
        {
            var spawnPoints = spawnPointsLayer.GetComponentsInChildren<SuperObject>();
            _spawnPoints = new SpawnPoints(spawnPoints);
        }

        public void GenerateFor(StageType stageType)
        {
            var count = ComputeCount();

            foreach (var spawnPoint in _spawnPoints.TakeSome(count))
                SpawnFor(stageType, spawnPoint);
        }

        private static int ComputeCount()
        {
            return Random.Range(0, 4);
        }

        private void SpawnFor(StageType stageType, Vector3 position)
        {
            var enemyType = _frequencies.GetRandomEnemyTypeFor(stageType);

            var enemy = _factory.Create(enemyType);
            enemy.Initialize();

            _repository.Add(enemy, position);
        }
    }
}
