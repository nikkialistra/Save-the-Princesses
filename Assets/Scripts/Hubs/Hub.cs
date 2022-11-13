using System;
using Cysharp.Threading.Tasks;
using Heroes;
using Infrastructure;
using Surrounding;
using Surrounding.Rooms;
using UnityEngine;
using Zenject;

namespace Hubs
{
    public class Hub : MonoBehaviour
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

        public async UniTaskVoid Initialize(Action onFinish)
        {
            await InitializeHubRoom();

            _hero.PlaceAt(_heroSpawnPoint.position);
            _hero.Activate();

            _exitHubTrigger.Enter += EnterDungeon;

            StartHub();

            onFinish?.Invoke();
        }

        private async UniTask InitializeHubRoom()
        {
            _hubRoom.Initialize(_hubRoomKind, transform);

            await UniTask.NextFrame();

            _hubRoom.SetupNavigation();
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
