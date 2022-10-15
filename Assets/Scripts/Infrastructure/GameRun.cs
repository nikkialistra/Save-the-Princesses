using Infrastructure.Loading;
using Saving.Progress.State;
using UI;
using static Saving.Progress.State.GameState;

namespace Infrastructure
{
    public class GameRun
    {
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingScreen _loadingScreen;

        public GameRun(SceneLoader sceneLoader, LoadingScreen loadingScreen)
        {
            _sceneLoader = sceneLoader;
            _loadingScreen = loadingScreen;
        }

        public void EnterDungeon()
        {
            _loadingScreen.Show();

            _sceneLoader.ReplaceAdditive(Dungeon.GetSceneName(), Hub.GetSceneName());
        }

        public void Finish()
        {
            _loadingScreen.Show();

            _sceneLoader.ReplaceAdditive(Hub.GetSceneName(), Dungeon.GetSceneName());
        }
    }
}
