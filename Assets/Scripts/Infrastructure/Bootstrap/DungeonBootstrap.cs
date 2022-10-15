using System;
using DungeonSystem;
using Surrounding;
using Surrounding.Rooms;
using UI;
using Zenject;

namespace Infrastructure.Bootstrap
{
    public class DungeonBootstrap : IInitializable, IDisposable
    {
        private DungeonControl _dungeonControl;

        private ActiveRepositories _activeRepositories;

        private LoadingScreen _loadingScreen;
        private Room _firstRoom;

        [Inject]
        public void Construct(DungeonControl dungeonControl, ActiveRepositories activeRepositories, LoadingScreen loadingScreen)
        {
            _dungeonControl = dungeonControl;

            _activeRepositories = activeRepositories;

            _loadingScreen = loadingScreen;
        }

        public void Initialize()
        {
            _dungeonControl.Initialize();
            _activeRepositories.SetStartRepositories(_firstRoom.Repositories);

            _loadingScreen.Hide();
        }

        public void Dispose()
        {
            _dungeonControl.Dispose();
            _activeRepositories.Dispose();
        }
    }
}
