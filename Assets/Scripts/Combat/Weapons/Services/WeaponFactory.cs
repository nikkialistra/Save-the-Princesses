using System.Collections.Generic;
using GameData.Weapons;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Combat.Weapons.Services
{
    public class WeaponFactory : SerializedMonoBehaviour
    {
        [SerializeField] private Dictionary<WeaponType, WeaponData> _weaponsMap = new();

        private DiContainer _diContainer;

        [Inject]
        public void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public Weapon Create(WeaponType weaponType, Transform parent)
        {
            var weaponData = _weaponsMap[weaponType];

            var weapon = _diContainer.InstantiatePrefabForComponent<Weapon>(weaponData.Prefab);
            weapon.Initialize(weaponData.Specs, parent);

            return weapon;
        }
    }
}
