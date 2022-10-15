using System;
using System.Collections;
using Common;
using Infrastructure.CoroutineRunners;
using UnityEngine.SceneManagement;

namespace Infrastructure.Loading
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void LoadAdditive(string name, Action onLoad = null)
        {
            _coroutineRunner.StartCoroutine(CLoadSceneAdditive(name, onLoad));
        }

        public void ReplaceAdditive(string nameTo, string nameFrom, Action onFinish = null)
        {
            _coroutineRunner.StartCoroutine(CReplaceSceneAdditive(nameTo, nameFrom, onFinish));
        }

        private IEnumerator CLoadSceneAdditive(string nextScene, Action onLoad = null)
        {
            ValidateOperation(nextScene);

            var loadNextScene = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);

            while (!loadNextScene.isDone)
                yield return null;

            onLoad?.Invoke();
        }

        private IEnumerator CReplaceSceneAdditive(string nextScene, string previousScene, Action onFinish = null)
        {
            ValidateOperation(nextScene);

            var unloadPreviousScene = SceneManager.UnloadSceneAsync(previousScene);
            var loadNextScene = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);

            while (!unloadPreviousScene.isDone || !loadNextScene.isDone)
                yield return null;

            onFinish?.Invoke();
        }

        private static void ValidateOperation(string nextScene)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
                throw new InvalidOperationException($"Trying to load already loaded scene: {nextScene}");
        }
    }
}
