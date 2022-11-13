using UnityEngine;

namespace Utilities.GameStart
{
    [CreateAssetMenu(fileName = "Game Start Configuration", menuName = "Editor/Game Start Configuration")]
    public class GameStartConfiguration : ScriptableObject
    {
        public StartScene StartScene;


#if UNITY_EDITOR

        private void OnValidate()
        {
            EditorGameStart.UpdateConfiguration();
        }

#endif
    }
}
