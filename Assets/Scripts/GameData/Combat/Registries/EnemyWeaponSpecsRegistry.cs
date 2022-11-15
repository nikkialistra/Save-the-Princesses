using System.Collections.Generic;
using Combat.Weapons;
using GameSystems;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameData.Combat.Registries
{
    public class EnemyWeaponSpecsRegistry : SerializedMonoBehaviour
    {
        [SerializeField] private Dictionary<WeaponType, EnemyWeaponSpecs> _weaponSpecsMap = new();

        private readonly GameControl _gameControl;

        public EnemyWeaponSpecsRegistry(GameControl gameControl)
        {
            _gameControl = gameControl;
        }

        public WeaponSpecs GetWeaponSpecs(WeaponType weaponType)
        {
            var enemyWeaponSpecs = _weaponSpecsMap[weaponType];

            return enemyWeaponSpecs[_gameControl.CurrentDifficulty];
        }
    }
}
