using Controls;
using Data.Enemies;
using Data.Princesses.Elements;
using Data.Princesses.Palettes;
using Enemies;
using Enemies.Services;
using Enemies.Services.Repositories;
using Heroes;
using Infrastructure.Bootstrap;
using Infrastructure.Controls;
using Princesses;
using Princesses.Services.Repositories;
using Saving.Progress;
using Saving.Saves;
using Sirenix.OdinInspector;
using Surrounding;
using Surrounding.Rooms;
using Trains;
using Trains.HandConnections;
using UI;
using UI.HealthBar;
using UI.Stats;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using InputControl = Controls.InputControl;

namespace Infrastructure.Installers.Game
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Hero _hero;
        [SerializeField] private Room _room;

        [Title("Surroundings")]
        [SerializeField] private Navigation _navigation;

        [Title("Character Factories")]
        [SerializeField] private Princess _princessPrefab;
        [SerializeField] private EnemyFactory _enemyFactory;

        [Title("Enemies Data")]
        [SerializeField] private EnemyFrequenciesRegistry _enemyFrequenciesRegistry;

        [Title("Princesses Data")]
        [SerializeField] private PrincessPalettesRegistry _princessPalettesRegistry;
        [SerializeField] private PrincessElementControllersRegistry _princessElementControllersRegistry;

        [Title("Train System")]
        [SerializeField] private Train _train;
        [SerializeField] private HandsSprites _handsSprites;

        [Title("Input")]
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private InputDevices _inputDevices;

        [Title("Game Interface")]
        [SerializeField] private HealthBarView _healthBarView;
        [SerializeField] private AmmoView _ammoView;
        [SerializeField] private StatsView _statsView;
        [SerializeField] private GoldView _goldView;

        [Title("Controls")]
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
            Container.BindInstance(_camera);
            Container.BindInterfacesAndSelfTo<Hero>().FromInstance(_hero);
            Container.BindInstance(_room);
            Container.BindInstance(_navigation);

            BindCharacterFactories();
            BindEnemiesData();
            BindPrincessesData();
            BindTrainSystem();
            BindRepositories();
            BindInput();
            BindUI();
            BindControls();
            BindGameRun();

            BindProgress();
            BindBootstrap();
        }

        private void BindCharacterFactories()
        {
            Container.BindFactory<Princess, Princess.Factory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab(_princessPrefab);

            Container.BindInstance(_enemyFactory);
        }

        private void BindTrainSystem()
        {
            Container.BindInstance(_train);
            Container.BindInstance(_handsSprites);
        }

        private void BindEnemiesData()
        {
            Container.BindInstance(_enemyFrequenciesRegistry);
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

        private void BindInput()
        {
            Container.BindInstance(_playerInput);
            Container.BindInstance(_inputDevices);
        }

        private void BindUI()
        {
            Container.BindInstance(_healthBarView);
            Container.BindInstance(_ammoView);
            Container.BindInstance(_statsView);
            Container.BindInstance(_goldView);
        }

        private void BindControls()
        {
            Container.BindInstance(_gameInterfaceControl);
            Container.BindInstance(_inputControl);
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
