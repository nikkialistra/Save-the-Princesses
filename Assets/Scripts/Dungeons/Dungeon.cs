using System.Collections;
using Heroes;
using Infrastructure;
using Surrounding;
using Surrounding.Rooms;
using UnityEngine;
using Zenject;

namespace Dungeons
{
    public class Dungeon : MonoBehaviour
    {
        public Room ActiveRoom { get; private set; }

        [SerializeField] private float _timeToFinishRun = 1.5f;

        private Hero _hero;
        private RoomGenerator _roomGenerator;

        private GameRun _gameRun;

        private ActiveRepositories _activeRepositories;

        [Inject]
        public void Construct(Hero hero, RoomGenerator roomGenerator,
            GameRun gameRun, ActiveRepositories activeRepositories)
        {
            _hero = hero;
            _roomGenerator = roomGenerator;

            _gameRun = gameRun;

            _activeRepositories = activeRepositories;
        }

        public void Initialize()
        {
            ActiveRoom = _roomGenerator.CreateFirstRoom();

            _hero.Dying += FinishRun;

            _hero.Activate();

            StartRun();
        }

        public void Dispose()
        {
            _roomGenerator.Dispose();

            _hero.Dying -= FinishRun;

            _hero.Deactivate();
        }

        private void StartRun()
        {
            _activeRepositories.FillRepositories(ActiveRoom.Repositories);
        }

        public void ChangeActiveRoomTo(Room room)
        {
            _activeRepositories.ChangeRepositories(room.Repositories);

            ActiveRoom = room;
        }

        private void FinishRun()
        {
            Dispose();
            StartCoroutine(CFinishRunAfterSomeTime());
        }

        private IEnumerator CFinishRunAfterSomeTime()
        {
            yield return new WaitForSeconds(_timeToFinishRun);

            _gameRun.Finish();
        }
    }
}
