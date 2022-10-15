using UI;

namespace Infrastructure.Loading
{
    public class GameLoading
    {
        private const string GameSceneName = "Game";

        private readonly SceneLoader _sceneLoader;
        private readonly LoadingScreen _loadingScreen;

        public GameLoading(SceneLoader sceneLoader, LoadingScreen loadingScreen)
        {
            _sceneLoader = sceneLoader;
            _loadingScreen = loadingScreen;
        }

        public void Load()
        {
            _loadingScreen.Show();

            _sceneLoader.LoadAdditive(GameSceneName);
        }
    }
}
