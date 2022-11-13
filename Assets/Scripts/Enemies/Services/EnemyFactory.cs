using System.Collections.Generic;
using GameData.Enemies;
using GameSystems;
using Sirenix.OdinInspector;
using Surrounding.Staging;
using UnityEngine;
using Zenject;

namespace Enemies.Services
{
    public class EnemyFactory : SerializedMonoBehaviour
    {
        [SerializeField] private Dictionary<EnemyType, EnemyData> _enemiesMap = new();

        private GameControl _gameControl;

        private DiContainer _diContainer;

        [Inject]
        public void Construct(GameControl gameControl, DiContainer diContainer)
        {
            _gameControl = gameControl;

            _diContainer = diContainer;
        }

        public Enemy Create(EnemyType enemyType)
        {
            var enemyData = _enemiesMap[enemyType];

            var enemy = _diContainer.InstantiatePrefabForComponent<Enemy>(enemyData.Prefab);

            enemy.Initialize(enemyData.InitialStats.For(_gameControl.CurrentDifficulty));

            return enemy;
        }
    }
}
