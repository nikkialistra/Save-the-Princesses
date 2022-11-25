using System.Collections;
using Cysharp.Threading.Tasks;
using GameData.Settings;
using GameSystems;
using GameSystems.Parameters;
using Heroes;
using Infrastructure.Controls;
using Staging;
using UnityEngine;
using Zenject;

namespace Dungeons
{
    public class Dungeon : MonoBehaviour
    {
        private Hero _heroFirst;
        private Hero _heroSecond;

        private Stages _stages;

        private GameRun _gameRun;

        private GameMode _gameMode;

        [Inject]
        public void Construct(Stages stages, GameRun gameRun)
        {
            _stages = stages;

            _gameRun = gameRun;
        }

        public async UniTask Initialize(HeroesControl heroesControl, GameMode gameMode)
        {
            _gameMode = gameMode;

            SetupHeroes(heroesControl);

            _heroFirst = heroesControl.First;
            _heroSecond = heroesControl.Second;

            await _stages.StartFirstStage();
        }

        private void SetupHeroes(HeroesControl heroesControl)
        {
            if (_gameMode == GameMode.Single)
                SetupHeroForSingle(heroesControl);
            else
                SetupHeroesForCoop(heroesControl);
        }

        private void SetupHeroForSingle(HeroesControl heroesControl)
        {
            _heroFirst = heroesControl.First;

            _heroFirst.Slain += FinishRun;
            _heroFirst.Active = true;
        }

        private void SetupHeroesForCoop(HeroesControl heroesControl)
        {
            _heroFirst = heroesControl.First;
            _heroSecond = heroesControl.Second;

            _heroFirst.Slain += OnHeroSlainInCoop;
            _heroSecond.Slain += OnHeroSlainInCoop;

            _heroFirst.Active = true;
            _heroSecond.Active = true;
        }

        public void Dispose()
        {
            if (_gameMode == GameMode.Single)
                DisposeHero();
            else
                DisposeHeroes();

            _stages.Dispose();
        }

        private void DisposeHero()
        {
            _heroFirst.Slain -= FinishRun;
            _heroFirst.Active = false;
        }

        private void DisposeHeroes()
        {
            _heroFirst.Slain -= FinishRun;
            _heroSecond.Slain -= FinishRun;

            _heroFirst.Active = false;
            _heroSecond.Active = false;
        }

        private void OnHeroSlainInCoop()
        {
            if (_heroFirst.Active || _heroSecond) return;

            FinishRun();
        }

        private void FinishRun()
        {
            StartCoroutine(CFinishRunAfterSomeTime());
        }

        private IEnumerator CFinishRunAfterSomeTime()
        {
            yield return new WaitForSeconds(GameSettings.General.TimeToFinishRun);

            _gameRun.Finish();
        }
    }
}
