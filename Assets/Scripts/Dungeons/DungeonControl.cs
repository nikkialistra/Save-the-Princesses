using System;
using Cysharp.Threading.Tasks;
using Heroes;
using UnityEngine;
using Zenject;

namespace Dungeons
{
    public class DungeonControl : MonoBehaviour
    {
        [SerializeField] private Transform _heroSpawnPoint;

        private Dungeon _dungeon;

        [Inject]
        public void Construct(Dungeon dungeon)
        {
            _dungeon = dungeon;
        }

        public async UniTaskVoid Initialize(Hero hero, Action onFinish)
        {
            await _dungeon.Initialize(hero);

            hero.PlaceAt(_heroSpawnPoint.position);

            onFinish();
        }

        public void Dispose()
        {
            _dungeon.Dispose();
        }
    }
}
