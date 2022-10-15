using System;
using Heroes;
using Infrastructure;
using Medium;
using Medium.Rooms;
using UnityEngine;
using Zenject;

namespace HubSystem
{
    public class Hub : MonoBehaviour, IInitializable, IDisposable
    {
        [SerializeField] private Room _hubRoom;
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
            _hero.PlaceAt(_heroSpawnPoint.position);
            _hero.Activate();

            _exitHubTrigger.Enter += EnterDungeon;
            _hubRoom.Spawn += StartHub;
        }

        public void Dispose()
        {
            _hero.Deactivate();

            _exitHubTrigger.Enter -= EnterDungeon;
            _hubRoom.Spawn -= StartHub;
        }

        private void StartHub()
        {
            _hubRoom.Spawn -= StartHub;

            _activeRepositories.SetStartRepositories(_hubRoom.Repositories);
        }

        private void EnterDungeon()
        {
            Dispose();
            _gameRun.EnterDungeon();
        }
    }
}
