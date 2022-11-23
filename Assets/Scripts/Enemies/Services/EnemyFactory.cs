using System.Collections.Generic;
using Combat.Weapons.Services;
using GameData.Enemies;
using GameData.Weapons.Registries;
using GameSystems;
using Heroes.Services;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Enemies.Services
{
    public class EnemyFactory : SerializedMonoBehaviour
    {
        [SerializeField] private Dictionary<EnemyType, EnemyData> _enemiesMap = new();

        private GameParameters _gameParameters;

        private EnemyWeaponsRegistry _weaponsRegistry;
        private WeaponFactory _weaponFactory;

        private HeroClosestFinder _heroClosestFinder;

        private DiContainer _diContainer;

        [Inject]
        public void Construct(GameParameters gameParameters, EnemyWeaponsRegistry weaponsRegistry,
            WeaponFactory weaponFactory, HeroClosestFinder heroClosestFinder, DiContainer diContainer)
        {
            _gameParameters = gameParameters;

            _weaponsRegistry = weaponsRegistry;
            _weaponFactory = weaponFactory;

            _heroClosestFinder = heroClosestFinder;

            _diContainer = diContainer;
        }

        public Enemy Create(EnemyType enemyType)
        {
            var enemyData = _enemiesMap[enemyType];

            var enemy = _diContainer.InstantiatePrefabForComponent<Enemy>(enemyData.Prefab);

            SetupEnemy(enemy, enemyType, enemyData);

            return enemy;
        }

        private void SetupEnemy(Enemy enemy, EnemyType enemyType, EnemyData enemyData)
        {
            var initialStats = enemyData.InitialStats.For(_gameParameters.CurrentDifficulty);

            enemy.Initialize(_heroClosestFinder, initialStats, enemyData.Specs);

            var weaponType = _weaponsRegistry.GetRandomWeaponTypeFor(enemyType);
            var weapon = _weaponFactory.Create(weaponType, enemy.Character);

            enemy.ChangeWeapon(weapon);

            enemy.Active = true;
        }
    }
}
