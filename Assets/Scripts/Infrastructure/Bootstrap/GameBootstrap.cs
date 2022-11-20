using System;
using Combat.Weapons;
using Heroes;
using Heroes.Services;
using Infrastructure.Controls;
using Infrastructure.Loading;
using Saving.Progress;
using Saving.Progress.State;
using Trains;
using Zenject;

namespace Infrastructure.Bootstrap
{
    public class GameBootstrap : IInitializable, IDisposable
    {
        private readonly HeroFactory _heroFactory;
        private readonly GameControls _gameControls;

        private readonly SceneLoader _sceneLoader;
        private readonly GameProgress _gameProgress;
        private readonly Train _train;

        private Hero _hero;

        public GameBootstrap(HeroFactory heroFactory, Train train, GameControls gameControls,
            SceneLoader sceneLoader, GameProgress gameProgress)
        {
            _heroFactory = heroFactory;
            _train = train;

            _gameControls = gameControls;

            _sceneLoader = sceneLoader;
            _gameProgress = gameProgress;
        }

        public void Initialize()
        {
            _hero = _heroFactory.CreateWith(WeaponType.None);
            _train.Initialize(_hero);

            _gameControls.Initialize(_hero);

            LoadGame();
        }

        public void Dispose()
        {
            _hero.Dispose();
            _train.Dispose();

            _gameControls.Dispose();
        }

        private void LoadGame()
        {
            _sceneLoader.LoadAdditive(_gameProgress.GameState.GetSceneName());
        }
    }
}
