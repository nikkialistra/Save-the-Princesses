using System;

namespace Medium.Stages
{
    public static class StageExtensions
    {
        public static string GetName(this Stage stage)
        {
            return stage switch
            {
                Stage.RuinedDungeon => "RuinedDungeon",
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
