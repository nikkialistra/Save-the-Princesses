using System;

namespace Staging
{
    public static class StageExtensions
    {
        public static string GetName(this StageType stageType)
        {
            return stageType switch
            {
                StageType.RuinedDungeon => "Ruined Dungeon",
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
