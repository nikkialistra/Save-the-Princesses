using System;
using Cysharp.Threading.Tasks;
using GameSystems;
using GameSystems.Parameters;
using Heroes;
using Infrastructure.Controls;
using Rooms;
using Surrounding;
using UnityEngine;
using Zenject;

namespace Hubs
{
    public class Hub : MonoBehaviour
    {
        [SerializeField] private Room _hubRoom;
        [SerializeField] private RoomKind _hubRoomKind;

        [Space]
        [SerializeField] private Transform _heroSpawnPointFirst;
        [SerializeField] private Transform _heroSpawnPointSecond;

        [Space]
        [SerializeField] private ZoneTrigger _exitHubTrigger;

        private Hero _heroFirst;
        private Hero _heroSecond;

        private GameRun _gameRun;
        private ActiveRepositories _activeRepositories;

        private GameParameters _gameParameters;

        [Inject]
        public void Construct(GameRun gameRun, ActiveRepositories activeRepositories, GameParameters gameParameters)
        {
            _gameRun = gameRun;
            _activeRepositories = activeRepositories;
            _gameParameters = gameParameters;
        }

        public async UniTaskVoid Initialize(HeroesControl heroesControl, Action onFinish)
        {
            await InitializeHubRoom();

            PlaceHeroes(heroesControl);

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

        private void PlaceHeroes(HeroesControl heroesControl)
        {
            if (_gameParameters.GameMode == GameMode.Single)
                PlaceHeroForSingle(heroesControl);
            else
                PlaceHeroesForCoop(heroesControl);
        }

        private void PlaceHeroForSingle(HeroesControl heroesControl)
        {
            _heroFirst = heroesControl.First;

            _heroFirst.PlaceAt(_heroSpawnPointFirst.position);
            _heroFirst.Active = true;
        }

        private void PlaceHeroesForCoop(HeroesControl heroesControl)
        {
            _heroFirst = heroesControl.First;
            _heroSecond = heroesControl.Second;

            _heroFirst.PlaceAt(_heroSpawnPointFirst.position);
            _heroSecond.PlaceAt(_heroSpawnPointSecond.position);

            _heroFirst.Active = true;
            _heroSecond.Active = true;
        }

        public void Dispose()
        {
            _heroFirst.Active = false;

            if (_heroSecond != null)
                _heroSecond.Active = false;

            _hubRoom.Dispose();

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
