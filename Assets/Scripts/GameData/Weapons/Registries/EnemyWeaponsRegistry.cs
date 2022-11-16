using System.Collections.Generic;
using Combat.Weapons;
using Enemies;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameData.Weapons.Registries
{
    public class EnemyWeaponsRegistry : SerializedMonoBehaviour
    {
        [SerializeField] private Dictionary<EnemyType, List<WeaponType>> _weaponsMap = new();

        public WeaponType GetRandomWeaponTypeFor(EnemyType enemyType)
        {
            var weapons = _weaponsMap[enemyType];

            if (weapons.Count == 0)
                return WeaponType.None;

            return weapons[Random.Range(0, weapons.Count)];
        }
    }
}
