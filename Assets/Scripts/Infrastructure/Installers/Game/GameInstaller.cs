using Combat.Weapons.Services;
using Enemies.Services;
using Enemies.Services.Repositories;
using GameConfig;
using GameData.Enemies.Spawning;
using GameData.Princesses.Appearance.Registries;
using GameData.Princesses.Spawning;
using GameData.Settings;
using GameData.Weapons.Registries;
using GameSystems;
using Heroes.Services;
using Infrastructure.Bootstrap;
using Infrastructure.Controls;
using Princesses.Services;
using Princesses.Services.Repositories;
using Saving.Progress;
using Saving.Saves;
using Sirenix.OdinInspector;
using Surrounding;
using Surrounding.Rooms.Services;
using Trains.HandConnections;
using UI;
using UI.HealthBar;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using InputControl = Controls.InputControl;

namespace Infrastructure.Installers.Game
{
    public class GameInstaller : MonoInstaller
    {
        [Title("Core")]
        [SerializeField] private GameSettings _gameSettings;
        [SerializeField] private PlayerInput _playerInput;

        [Title("General")]
        [SerializeField] private Camera _camera;
        [SerializeField] private RoomFactory _roomFactory;
        [SerializeField] private Navigation _navigation;

        [Title("Characters")]
        [SerializeField] private HeroFactory _heroFactory;
        [SerializeField] private PrincessFactory _princessFactory;
        [SerializeField] private EnemyFactory _enemyFactory;

        [Title("Weapons")]
        [SerializeField] private EnemyWeaponsRegistry _enemyWeaponsRegistry;
        [SerializeField] private WeaponFactory _weaponFactory;

        [Title("Princesses Data")]
        [SerializeField] private PrincessPalettesRegistry _princessPalettesRegistry;
        [SerializeField] private PrincessElementControllersRegistry _princessElementControllersRegistry;

        [Title("Train System")]
        [SerializeField] private HandsSprites _handsSprites;

        [Title("Game Interface")]
        [SerializeField] private HealthBarView _healthBarView;
        [SerializeField] private GoldView _goldView;

        [Title("General Controls")]
        [SerializeField] private GameInterfaceControl _gameInterfaceControl;
        [SerializeField] private InputControl _inputControl;

        private GameSaves _gameSaves;

        [Inject]
        public void Construct(GameSaves gameSaves)
        {
            _gameSaves = gameSaves;
        }

        public override void InstallBindings()
        {
            BindCore();
            BindGeneral();

            BindHeroes();
            BindCharactersPicking();
            BindCharacters();
            BindWeapons();
            BindRepositories();

            BindPrincessesData();
            BindTrainSystem();

            BindUI();
            BindGeneralControls();

            BindGameRun();

            BindProgress();
            BindBootstrap();
        }

        private void BindCore()
        {
            Container.BindInterfacesAndSelfTo<GameSettings>().FromInstance(_gameSettings);
            Container.BindInstance(_playerInput);
        }

        private void BindGeneral()
        {
            Container.BindInstance(_camera);
            Container.BindInstance(_roomFactory);
            Container.BindInstance(_navigation);
        }

        private void BindHeroes()
        {
            Container.Bind<HeroClosestFinder>().AsSingle();
            Container.BindInterfacesAndSelfTo<HeroesControl>().AsSingle();
        }

        private void BindCharactersPicking()
        {
            Container.Bind<PrincessPicking>().AsSingle();
            Container.Bind<EnemyPicking>().AsSingle();
        }

        private void BindCharacters()
        {
            Container.BindInstance(_heroFactory);
            Container.BindInstance(_princessFactory);
            Container.BindInstance(_enemyFactory);
        }

        private void BindWeapons()
        {
            Container.BindInstance(_enemyWeaponsRegistry);
            Container.BindInstance(_weaponFactory);
        }

        private void BindTrainSystem()
        {
            Container.BindInstance(_handsSprites);
        }

        private void BindPrincessesData()
        {
            Container.BindInstance(_princessPalettesRegistry);
            Container.BindInstance(_princessElementControllersRegistry);
        }

        private void BindRepositories()
        {
            Container.Bind<PrincessActiveRepository>().AsSingle();
            Container.Bind<EnemyActiveRepository>().AsSingle();

            Container.Bind<ActiveRepositories>().AsSingle();
        }

        private void BindUI()
        {
            Container.BindInstance(_healthBarView);
            Container.BindInstance(_goldView);
        }

        private void BindGeneralControls()
        {
            Container.Bind<GameParameters>().AsSingle();
            Container.BindInstance(_gameInterfaceControl);
            Container.BindInterfacesAndSelfTo<InputControl>().FromInstance(_inputControl);
            Container.Bind<GameControls>().AsSingle();
        }

        private void BindGameRun()
        {
            Container.Bind<GameRun>().AsSingle();
        }

        private void BindProgress()
        {
            var progress = _gameSaves?.CurrentSave.Progress;

            Container.Bind<GameProgress>().FromInstance(progress);
        }

        private void BindBootstrap()
        {
            Container.BindInterfacesTo<GameBootstrap>().AsSingle().NonLazy();
        }
    }
}
