using System.Collections.Generic;
using Combat.Weapons.Services;
using GameData.Enemies;
using GameData.Weapons.Registries;
using GameSystems;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Enemies.Services
{
    public class EnemyFactory : SerializedMonoBehaviour
    {
        [SerializeField] private Dictionary<EnemyType, EnemyData> _enemiesMap = new();

        private GameControl _gameControl;

        private EnemyWeaponsRegistry _weaponsRegistry;
        private WeaponFactory _weaponFactory;

        private DiContainer _diContainer;

        [Inject]
        public void Construct(GameControl gameControl, EnemyWeaponsRegistry weaponsRegistry,
            WeaponFactory weaponFactory, DiContainer diContainer)
        {
            _gameControl = gameControl;

            _weaponsRegistry = weaponsRegistry;
            _weaponFactory = weaponFactory;

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
            var weaponType = _weaponsRegistry.GetRandomWeaponTypeFor(enemyType);

            var weapon = _weaponFactory.Create(weaponType, enemy.transform);

            enemy.Initialize(enemyData.InitialStats.For(_gameControl.CurrentDifficulty));
            enemy.SetWeapon(weapon);
            enemy.Active = true;
        }
    }
}
