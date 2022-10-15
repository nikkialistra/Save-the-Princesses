using Infrastructure.Loading;
using Saving.Saves;
using Zenject;

namespace Infrastructure.Bootstrap
{
    public class SessionBootstrap : IInitializable
    {
        private readonly GameLoading _gameLoading;
        private readonly GameSaves _gameSaves;

        public SessionBootstrap(GameLoading gameLoading, GameSaves gameSaves)
        {
            _gameLoading = gameLoading;
            _gameSaves = gameSaves;
        }

        public void Initialize()
        {
            if (_gameSaves.LoadingFromSave == false)
                CreateGame();

            _gameLoading.Load();
        }

        private void CreateGame()
        {
            _gameSaves.CreateNewSave();
        }
    }
}
