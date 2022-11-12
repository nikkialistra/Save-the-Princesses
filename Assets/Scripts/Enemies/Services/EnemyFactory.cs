using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Enemies.Services
{
    public class EnemyFactory : SerializedMonoBehaviour
    {
        [SerializeField] private Dictionary<EnemyType, Enemy> _enemiesMap = new();

        private DiContainer _diContainer;

        [Inject]
        public void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public Enemy Create(EnemyType enemyType)
        {
            var enemyPrefab = _enemiesMap[enemyType];

            var enemy = _diContainer.InstantiatePrefabForComponent<Enemy>(enemyPrefab);

            return enemy;
        }
    }
}
