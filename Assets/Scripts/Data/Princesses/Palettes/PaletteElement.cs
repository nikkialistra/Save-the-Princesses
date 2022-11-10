using System;
using Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data.Princesses.Palettes
{
    [Serializable]
    public class PaletteElement<T> : ChanceSetup where T : ScriptableObject
    {
        [HorizontalGroup("Split", 150, MarginRight = 20)]
        [VerticalGroup("Split/Left", PaddingTop = 11)]
        [HideLabel]
        public T Palette;
    }
}
