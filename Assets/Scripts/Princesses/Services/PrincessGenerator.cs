using GameData.Princesses.Spawning;
using GameData.Princesses.Spawning.Frequencies;
using Princesses.Services.Repositories;
using Princesses.Types;
using SuperTiled2Unity;
using Surrounding.Staging;
using UnityEngine;

namespace Princesses.Services
{
    public class PrincessGenerator
    {
        private Transform[] _spawnPoints;

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
            _spawnPoints = spawnPointsLayer.GetComponentsInChildren<Transform>();
        }

        public void Generate(PrincessCategoryRoomFrequencies categoryRoomFrequencies, StageType stageType)
        {
            foreach (var spawnPoint in _spawnPoints)
                SpawnFor(spawnPoint.position, categoryRoomFrequencies, stageType);
        }

        private void SpawnFor(Vector3 position, PrincessCategoryRoomFrequencies categoryRoomFrequencies, StageType stageType)
        {
            var princessType = _picking.GetRandomPrincessType(categoryRoomFrequencies);

            if (princessType == PrincessType.None) return;

            var princess = _factory.Create(princessType, stageType);

            _repository.Add(princess, position);
        }
    }
}
