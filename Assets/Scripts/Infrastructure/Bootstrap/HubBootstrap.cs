using System;
using Heroes;
using HubSystem;
using Medium;
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
            _hub.Initialize();

            _loadingScreen.Hide();
        }

        public void Dispose()
        {
            _hub.Dispose();
            _activeRepositories.Dispose();
        }
    }
}
