using System.Collections.Generic;
using System.Linq;
using Enemies.Services.Repositories;
using SuperTiled2Unity;
using UnityEngine;

namespace Enemies.Services
{
    public class EnemyGenerator
    {
        private SuperObject[] _spawnPoints;

        private HashSet<SuperObject> _freeSpawnPoints;

        private EnemyActiveRepository _activeRepository;

        public EnemyGenerator(EnemyActiveRepository activeRepository)
        {
            _activeRepository = activeRepository;
        }

        public void Initialize(SuperObjectLayer spawnPointsLayer)
        {
            _spawnPoints = spawnPointsLayer.GetComponents<SuperObject>();
            _freeSpawnPoints = _spawnPoints.ToHashSet();
        }

        public void Generate()
        {
            var count = ComputeCount();

            for (int i = 0; i < count; i++)
            {
                if (_freeSpawnPoints.Count == 0) return;

                var randomPoint = GetRandomPoint();

                SpawnAt(randomPoint);
            }
        }

        private int ComputeCount()
        {
            return Random.Range(0, 4);
        }

        private Vector3 GetRandomPoint()
        {
            var randomIndex = Random.Range(0, _freeSpawnPoints.Count);
            var randomSuperObject = _freeSpawnPoints.ElementAt(randomIndex);

            _freeSpawnPoints.Remove(randomSuperObject);

            return randomSuperObject.transform.position;
        }

        private void SpawnAt(Vector3 randomPoint) { }
    }
}
