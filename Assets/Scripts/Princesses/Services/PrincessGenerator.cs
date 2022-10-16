using Princesses.Services.Repositories;
using SuperTiled2Unity;
using Surrounding;
using Surrounding.Rooms;
using UnityEngine;

namespace Princesses.Services
{
    public class PrincessGenerator
    {
        private PrincessActiveRepository PrincessActiveRepository => _activeRepositories.Princesses;

        private SpawnPoints _spawnPoints;

        private readonly Princess.Factory _princessFactory;

        private readonly ActiveRepositories _activeRepositories;

        public PrincessGenerator(Princess.Factory princessFactory, ActiveRepositories activeRepositories)
        {
            _princessFactory = princessFactory;
            _activeRepositories = activeRepositories;
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
                SpawnAt(spawnPoint);
        }

        private static int ComputeCount()
        {
            return Random.Range(0, 4);
        }

        private void SpawnAt(Vector3 position)
        {
            var princess = _princessFactory.Create();

            PrincessActiveRepository.Add(princess, position);
        }
    }
}
