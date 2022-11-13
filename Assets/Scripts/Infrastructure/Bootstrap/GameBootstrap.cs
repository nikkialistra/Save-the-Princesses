using System;
using Heroes;
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
        private readonly Hero _hero;
        private readonly GameControls _gameControls;

        private readonly SceneLoader _sceneLoader;
        private readonly GameProgress _gameProgress;
        private readonly Train _train;

        public GameBootstrap(Hero hero, Train train, GameControls gameControls,
            SceneLoader sceneLoader, GameProgress gameProgress)
        {
            _hero = hero;
            _train = train;

            _gameControls = gameControls;

            _sceneLoader = sceneLoader;
            _gameProgress = gameProgress;
        }

        public void Initialize()
        {
            _hero.Initialize();
            _train.Initialize();

            _gameControls.Initialize(_hero.Attacker);

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
