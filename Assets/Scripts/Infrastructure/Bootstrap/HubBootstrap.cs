using System;
using Hubs;
using Surrounding;
using UI;
using Zenject;

namespace Infrastructure.Bootstrap
{
    public class HubBootstrap : IInitializable, IDisposable
    {
        private Hub _hub;

        private ActiveRepositories _activeRepositories;

        private LoadingScreen _loadingScreen;

        [Inject]
        public void Construct(Hub hub, ActiveRepositories activeRepositories, LoadingScreen loadingScreen)
        {
            _hub = hub;

            _activeRepositories = activeRepositories;

            _loadingScreen = loadingScreen;
        }

        public void Initialize()
        {
            _hub.Initialize(HideLoadingScreen).Forget();
        }

        private void HideLoadingScreen()
        {
            _loadingScreen.Hide();
        }

        public void Dispose()
        {
            _hub.Dispose();
            _activeRepositories.Dispose();
        }
    }
}
