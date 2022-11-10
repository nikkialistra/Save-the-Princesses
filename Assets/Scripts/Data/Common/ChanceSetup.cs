using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Common
{
    [Serializable]
    public class ChanceSetup
    {
        [VerticalGroup("Split/Right", order: 1)]
        [LabelWidth(110)]
        public float RelativeChance = 1;

        [VerticalGroup("Split/Right", order: 1)]
        [LabelWidth(110)]
        [ReadOnly]
        public string PercentChance = "100%";

        public float Chance
        {
            get => _chance;
            set
            {
                _chance = value;
                PercentChance = $"{_chance * 100:0.##}%";
            }
        }

        [HideInInspector]
        [SerializeField] private float _chance;
    }
}
