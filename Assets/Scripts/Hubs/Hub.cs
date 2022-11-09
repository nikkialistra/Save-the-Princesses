using System;
using Heroes;
using Infrastructure;
using Surrounding;
using Surrounding.Rooms;
using UnityEngine;
using Zenject;

namespace Hubs
{
    public class Hub : MonoBehaviour, IInitializable, IDisposable
    {
        [SerializeField] private Room _hubRoom;
        [SerializeField] private RoomKind _hubRoomKind;

        [Space]
        [SerializeField] private Transform _heroSpawnPoint;
        [SerializeField] private ZoneTrigger _exitHubTrigger;

        private Hero _hero;

        private GameRun _gameRun;
        private ActiveRepositories _activeRepositories;

        [Inject]
        public void Construct(Hero hero, GameRun gameRun, ActiveRepositories activeRepositories)
        {
            _hero = hero;

            _gameRun = gameRun;
            _activeRepositories = activeRepositories;
        }

        public void Initialize()
        {
            _hubRoom.Initialize(_hubRoomKind, transform);
            _hubRoom.SetupNavigation();

            _hero.PlaceAt(_heroSpawnPoint.position);
            _hero.Activate();

            _exitHubTrigger.Enter += EnterDungeon;

            StartHub();
        }

        public void Dispose()
        {
            _hubRoom.Dispose();

            _hero.Deactivate();

            _exitHubTrigger.Enter -= EnterDungeon;
        }

        private void StartHub()
        {
            _activeRepositories.FillRepositories(_hubRoom.Repositories);
        }

        private void EnterDungeon()
        {
            _exitHubTrigger.Enter -= EnterDungeon;

            _gameRun.EnterDungeon();
        }
    }
}
