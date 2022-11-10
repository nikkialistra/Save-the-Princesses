using System;
using Common;
using Enemies;
using Sirenix.OdinInspector;

namespace Data.Enemies
{
    [Serializable]
    public class EnemyFrequency : ChanceSetup
    {
        [HorizontalGroup("Split", 150, MarginRight = 20)]
        [VerticalGroup("Split/Left", PaddingTop = 11)]
        [HideLabel]
        public Enemy Enemy;
    }
}
