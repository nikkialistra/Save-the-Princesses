using System;
using Hubs;
using Infrastructure.Controls;
using Surrounding;
using UI;
using Zenject;

namespace Infrastructure.Bootstrap
{
    public class HubBootstrap : IInitializable, IDisposable
    {
        private Hub _hub;
        private HeroesControl _heroesControl;

        private ActiveRepositories _activeRepositories;

        private LoadingScreen _loadingScreen;

        [Inject]
        public void Construct(Hub hub, HeroesControl heroesControl, ActiveRepositories activeRepositories, LoadingScreen loadingScreen)
        {
            _hub = hub;
            _heroesControl = heroesControl;

            _activeRepositories = activeRepositories;

            _loadingScreen = loadingScreen;
        }

        public void Initialize()
        {
            _hub.Initialize(_heroesControl, HideLoadingScreen).Forget();
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
