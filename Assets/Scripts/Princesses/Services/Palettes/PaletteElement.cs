using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Princesses.Services.Palettes
{
    [Serializable]
    public class PaletteElement<T> where T : ScriptableObject
    {
        [HorizontalGroup("Split", 150, MarginRight = 20)]
        [VerticalGroup("Split/Left", PaddingTop = 11)]
        [HideLabel]
        public T Palette;

        [VerticalGroup("Split/Right")]
        [LabelWidth(110)]
        public float RelativeChance = 1;

        [VerticalGroup("Split/Right")]
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
