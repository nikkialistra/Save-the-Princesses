using System.Collections;
using Cysharp.Threading.Tasks;
using GameData.Settings;
using Heroes;
using Infrastructure;
using Surrounding.Staging;
using UnityEngine;
using Zenject;

namespace Dungeons
{
    public class Dungeon : MonoBehaviour
    {
        private Hero _hero;
        private Stages _stages;

        private GameRun _gameRun;

        [Inject]
        public void Construct(Hero hero, Stages stages, GameRun gameRun)
        {
            _hero = hero;
            _stages = stages;

            _gameRun = gameRun;
        }

        public async UniTask Initialize()
        {
            await _stages.StartFirstStage();

            _hero.Slain += FinishRun;

            _hero.Active = true;
        }

        public void Dispose()
        {
            _stages.Dispose();

            _hero.Slain -= FinishRun;

            _hero.Active = false;
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
