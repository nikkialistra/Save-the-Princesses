﻿using Data.Enemies;
using Enemies.Services.Repositories;
using SuperTiled2Unity;
using UnityEngine;

namespace Enemies.Services
{
    public class EnemyGenerator
    {
        private SuperObject[] _spawnPoints;

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
            _spawnPoints = spawnPointsLayer.GetComponentsInChildren<SuperObject>();
        }

        public void Generate(EnemyRoomFrequencies enemyRoomFrequencies)
        {
            foreach (var spawnPoint in _spawnPoints)
                SpawnFor(spawnPoint.transform.position, enemyRoomFrequencies);
        }

        private void SpawnFor(Vector3 position, EnemyRoomFrequencies enemyRoomFrequencies)
        {
            var enemyType = _picking.GetRandomEnemyType(enemyRoomFrequencies);

            if (enemyType == EnemyType.None) return;

            var enemy = _factory.Create(enemyType);
            enemy.Initialize();

            _repository.Add(enemy, position);
        }
    }
}
