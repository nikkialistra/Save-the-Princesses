using System;
using Cysharp.Threading.Tasks;
using Dungeons;
using Surrounding.Rooms;
using UI;
using Zenject;

namespace Infrastructure.Bootstrap
{
    public class DungeonBootstrap : IInitializable, IDisposable
    {
        private DungeonControl _dungeonControl;

        private LoadingScreen _loadingScreen;
        private Room _firstRoom;

        [Inject]
        public void Construct(DungeonControl dungeonControl, LoadingScreen loadingScreen)
        {
            _dungeonControl = dungeonControl;

            _loadingScreen = loadingScreen;
        }

        public void Initialize()
        {
            _dungeonControl.Initialize(HideLoadingScreen).Forget();
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
