using System.Collections.Generic;
using Combat.Weapons;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameData.Combat.Registries
{
    public class HeroWeaponSpecsRegistry : SerializedMonoBehaviour
    {
        [SerializeField] private Dictionary<WeaponType, WeaponSpecs> _weaponSpecsMap = new();

        public WeaponSpecs GetWeaponSpecs(WeaponType weaponType)
        {
            return _weaponSpecsMap[weaponType];
        }
    }
}
