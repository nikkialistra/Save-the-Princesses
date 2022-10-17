﻿using Controls;
using Enemies.Services.Repositories;
using Heroes;
using Infrastructure.Bootstrap;
using Infrastructure.Controls;
using Infrastructure.CoroutineRunners;
using Surrounding;
using Surrounding.Rooms;
using Princesses.Services.Elements;
using Princesses.Services.Palettes;
using Princesses.Services.Repositories;
using Saving.Progress;
using Saving.Saves;
using Saving.Settings;
using Sirenix.OdinInspector;
using Trains;
using Trains.HandConnections;
using UI;
using UI.HealthBar;
using UI.Stats;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using InputControl = Controls.InputControl;

namespace Infrastructure.CompositionRoot.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Hero _hero;
        [SerializeField] private Room _room;

        [Title("Train System")]
        [SerializeField] private Train _train;
        [SerializeField] private HandsSprites _handsSprites;

        [Title("Princesses")]
        [SerializeField] private PrincessPalettesRegistry _princessPalettesRegistry;
        [SerializeField] private PrincessElementControllersRegistry _princessElementControllersRegistry;

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

            BindTrainSystem();
            BindPrincesses();
            BindRepositories();
            BindInput();
            BindUI();
            BindControls();
            BindGameRun();

            BindProgress();
            BindBootstrap();
        }

        private void BindTrainSystem()
        {
            Container.BindInstance(_train);
            Container.BindInstance(_handsSprites);
        }

        private void BindPrincesses()
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