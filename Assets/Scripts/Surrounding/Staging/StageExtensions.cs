using System;

namespace Surrounding.Staging
{
    public static class StageExtensions
    {
        public static string GetName(this StageType stageType)
        {
            return stageType switch
            {
                StageType.RuinedDungeon => "RuinedDungeon",
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
