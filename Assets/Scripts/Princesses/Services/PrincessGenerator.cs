﻿using Princesses.Services.Repositories;
using SuperTiled2Unity;
using Surrounding.Rooms;
using UnityEngine;

namespace Princesses.Services
{
    public class PrincessGenerator
    {
        private readonly Princess.Factory _princessFactory;
        private SpawnPoints _spawnPoints;

        private readonly PrincessActiveRepository _activeRepository;

        public PrincessGenerator(PrincessActiveRepository activeRepository)
        {
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
            var enemy = _princessFactory.Create();

            _activeRepository.Add(enemy, position);
        }
    }
}
