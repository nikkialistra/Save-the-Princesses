using System;
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
        private readonly GameControls _gameControls;

        private readonly SceneLoader _sceneLoader;
        private readonly GameProgress _gameProgress;
        private readonly Train _train;

        public GameBootstrap(GameControls gameControls,
            SceneLoader sceneLoader, GameProgress gameProgress)
        {
            _gameControls = gameControls;

            _sceneLoader = sceneLoader;
            _gameProgress = gameProgress;
        }

        public void Initialize()
        {
            _gameControls.Initialize();

            LoadGame();
        }

        public void Dispose()
        {
            _train.Dispose();

            _gameControls.Dispose();
        }

        private void LoadGame()
        {
            _sceneLoader.LoadAdditive(_gameProgress.GameState.GetSceneName());
        }
    }
}
