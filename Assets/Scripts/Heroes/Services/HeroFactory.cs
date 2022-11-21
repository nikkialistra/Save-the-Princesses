using Combat.Weapons;
using Combat.Weapons.Services;
using GameData.Heroes;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Heroes.Services
{
    public class HeroFactory : SerializedMonoBehaviour
    {
        [SerializeField] private Hero _heroPrefab;
        [SerializeField] private HeroInitialStats _initialStats;

        private WeaponFactory _weaponFactory;

        private DiContainer _diContainer;

        [Inject]
        public void Construct(WeaponFactory weaponFactory, DiContainer diContainer)
        {
            _weaponFactory = weaponFactory;

            _diContainer = diContainer;
        }

        public Hero CreateWith(WeaponType weaponType)
        {
            var hero = _diContainer.InstantiatePrefabForComponent<Hero>(_heroPrefab);

            hero.Initialize(_initialStats.InitialStats);

            var weapon = _weaponFactory.Create(weaponType, hero.Character);
            hero.SetWeapon(weapon);

            return hero;
        }
    }
}
