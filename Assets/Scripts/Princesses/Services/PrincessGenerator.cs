using Data.Princesses.Spawning;
using Data.Princesses.Spawning.Frequencies;
using Princesses.Services.Repositories;
using Princesses.Types;
using SuperTiled2Unity;
using UnityEngine;

namespace Princesses.Services
{
    public class PrincessGenerator
    {
        private SuperObject[] _spawnPoints;

        private readonly PrincessPicking _picking;

        private readonly PrincessFactory _factory;

        private readonly PrincessRoomRepository _repository;

        public PrincessGenerator(PrincessPicking picking, PrincessFactory factory, PrincessRoomRepository repository)
        {
            _picking = picking;
            _factory = factory;
            _repository = repository;
        }

        public void Initialize(SuperObjectLayer spawnPointsLayer)
        {
            _spawnPoints = spawnPointsLayer.GetComponentsInChildren<SuperObject>();
        }

        public void Generate(PrincessCategoryRoomFrequencies categoryRoomFrequencies)
        {
            foreach (var spawnPoint in _spawnPoints)
                SpawnFor(spawnPoint.transform.position, categoryRoomFrequencies);
        }

        private void SpawnFor(Vector3 position, PrincessCategoryRoomFrequencies categoryRoomFrequencies)
        {
            var princessType = PrincessPicking.GetRandomPrincessType(categoryRoomFrequencies);

            if (princessType == PrincessType.None) return;

            var princess = _factory.Create(princessType);

            princess.Initialize();

            _repository.Add(princess, position);
        }
    }
}
