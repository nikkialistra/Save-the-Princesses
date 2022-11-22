using Combat.Weapons;
using Combat.Weapons.Services;
using GameData.Heroes;
using Sirenix.OdinInspector;
using Trains;
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

        public Hero CreateWith(WeaponType weaponType, string containerName)
        {
            var heroContainer = _diContainer.CreateEmptyGameObject(containerName).transform;

            var hero = _diContainer.InstantiatePrefabForComponent<Hero>(_heroPrefab);

            Initialize(hero, heroContainer, weaponType);

            var train = _diContainer.InstantiateComponentOnNewGameObject<Train>();
            train.transform.parent = heroContainer;

            return hero;
        }

        private void Initialize(Hero hero, Transform heroContainer, WeaponType weaponType)
        {
            hero.name = "Hero";
            hero.transform.parent = heroContainer;

            hero.Initialize(_initialStats.InitialStats);

            var weapon = _weaponFactory.Create(weaponType, hero.Character);
            hero.SetWeapon(weapon);
        }
    }
}
