using System;
using Dungeons;
using Infrastructure.Controls;
using Rooms;
using UI;
using Zenject;

namespace Infrastructure.Bootstrap
{
    public class DungeonBootstrap : IInitializable, IDisposable
    {
        private DungeonControl _dungeonControl;
        private HeroesControl _heroesControl;

        private LoadingScreen _loadingScreen;
        private Room _firstRoom;

        [Inject]
        public void Construct(DungeonControl dungeonControl, HeroesControl heroesControl, LoadingScreen loadingScreen)
        {
            _dungeonControl = dungeonControl;
            _heroesControl = heroesControl;

            _loadingScreen = loadingScreen;
        }

        public void Initialize()
        {
            _dungeonControl.Initialize(_heroesControl, HideLoadingScreen).Forget();
        }

        private void HideLoadingScreen()
        {
            _loadingScreen.Hide();
        }

        public void Dispose()
        {
            _dungeonControl.Dispose();
        }
    }
}
