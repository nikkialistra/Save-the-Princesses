using Heroes;
using Surrounding.Rooms;
using UnityEngine;
using Zenject;

namespace DungeonSystem
{
    public class DungeonControl : MonoBehaviour
    {
        [SerializeField] private Transform _heroSpawnPoint;

        private Dungeon _dungeon;
        private Hero _hero;

        [Inject]
        public void Construct(Dungeon dungeon, Hero hero)
        {
            _dungeon = dungeon;
            _hero = hero;
        }

        public void Initialize()
        {
            _dungeon.Initialize();

            _hero.PlaceAt(_heroSpawnPoint.position);
        }

        public void Dispose()
        {
            _dungeon.Dispose();
        }
    }
}
