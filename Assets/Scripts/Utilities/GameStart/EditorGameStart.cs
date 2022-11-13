using System;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Utilities.GameStart
{
    [InitializeOnLoad]
    public static class EditorGameStart
    {
        private const string MainMenuScenePath = "Assets/Scenes/MainMenu.unity";
        private const string BootstrapScenePath = "Assets/Scenes/Bootstrap.unity";

        private const string ConfigurationPath = "Assets/GameData/Settings/Game Start Configuration.asset";

        static EditorGameStart()
        {
            if (EditorSceneManager.playModeStartScene != null) return;

            UpdateConfiguration();
        }

        public static void UpdateConfiguration()
        {
            var startSceneAsset = GetStartSceneAsset();

            EditorSceneManager.playModeStartScene = startSceneAsset;
        }

        private static SceneAsset GetStartSceneAsset()
        {
            var configuration = AssetDatabase.LoadAssetAtPath<GameStartConfiguration>(ConfigurationPath);

            var startSceneAssetPath = configuration.StartScene switch {
                StartScene.MainMenu => MainMenuScenePath,
                StartScene.Bootstrap => BootstrapScenePath,
                _ => throw new ArgumentOutOfRangeException()
            };

            return AssetDatabase.LoadAssetAtPath<SceneAsset>(startSceneAssetPath);
        }
    }
}
