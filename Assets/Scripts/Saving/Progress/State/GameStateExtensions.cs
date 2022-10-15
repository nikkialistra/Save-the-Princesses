using System;

namespace Saving.Progress.State
{
    public static class GameStateExtensions
    {
        public static string GetSceneName(this GameState gameState)
        {
            return gameState switch {
                GameState.Hub => "Hub",
                GameState.Dungeon => "Dungeon",
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
