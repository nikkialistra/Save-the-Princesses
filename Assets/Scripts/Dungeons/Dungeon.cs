using System.Collections;
using Heroes;
using Infrastructure;
using Surrounding.Staging;
using UnityEngine;
using Zenject;

namespace Dungeons
{
    public class Dungeon : MonoBehaviour
    {
        [SerializeField] private float _timeToFinishRun = 1.5f;

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

        public void Initialize()
        {
            _hero.Slain += FinishRun;

            _hero.Activate();

            StartRun();
        }

        public void Dispose()
        {
            _stages.Dispose();

            _hero.Slain -= FinishRun;

            _hero.Deactivate();
        }

        private void StartRun()
        {
            _stages.StartFirstStage();
        }

        private void FinishRun()
        {
            StartCoroutine(CFinishRunAfterSomeTime());
        }

        private IEnumerator CFinishRunAfterSomeTime()
        {
            yield return new WaitForSeconds(_timeToFinishRun);

            _gameRun.Finish();
        }
    }
}
