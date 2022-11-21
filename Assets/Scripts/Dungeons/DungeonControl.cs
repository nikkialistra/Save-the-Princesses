using System;
using Cysharp.Threading.Tasks;
using GameSystems;
using GameSystems.Parameters;
using Infrastructure.Controls;
using UnityEngine;
using Zenject;

namespace Dungeons
{
    public class DungeonControl : MonoBehaviour
    {
        [SerializeField] private Transform _heroSpawnPointFirst;
        [SerializeField] private Transform _heroSpawnPointSecond;

        private Dungeon _dungeon;

        private GameParameters _gameParameters;

        [Inject]
        public void Construct(Dungeon dungeon, GameParameters gameParameters)
        {
            _gameParameters = gameParameters;
            _dungeon = dungeon;
        }

        public async UniTaskVoid Initialize(HeroesControl heroesControl, Action onFinish)
        {
            await _dungeon.Initialize(heroesControl, _gameParameters.GameMode);

            PlaceHeroes(heroesControl);

            onFinish();
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
            var heroFirst = heroesControl.First;

            heroFirst.PlaceAt(_heroSpawnPointFirst.position);
            heroFirst.Active = true;
        }

        private void PlaceHeroesForCoop(HeroesControl heroesControl)
        {
            var heroFirst = heroesControl.First;
            var heroSecond = heroesControl.Second;

            heroFirst.PlaceAt(_heroSpawnPointFirst.position);
            heroSecond.PlaceAt(_heroSpawnPointSecond.position);

            heroFirst.Active = true;
            heroSecond.Active = true;
        }

        public void Dispose()
        {
            _dungeon.Dispose();
        }
    }
}
