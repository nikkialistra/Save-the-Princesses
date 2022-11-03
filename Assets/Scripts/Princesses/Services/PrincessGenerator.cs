using Princesses.Services.Repositories;
using SuperTiled2Unity;
using Surrounding.Rooms;
using UnityEngine;

namespace Princesses.Services
{
    public class PrincessGenerator
    {
        private SpawnPoints _spawnPoints;

        private readonly Princess.Factory _princessFactory;

        private readonly PrincessRoomRepository _repository;

        public PrincessGenerator(Princess.Factory princessFactory, PrincessRoomRepository repository)
        {
            _princessFactory = princessFactory;
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
                SpawnAt(spawnPoint);
        }

        private static int ComputeCount()
        {
            return Random.Range(0, 4);
        }

        private void SpawnAt(Vector3 position)
        {
            var princess = _princessFactory.Create();
            princess.Initialize();

            _repository.Add(princess, position);
        }
    }
}
