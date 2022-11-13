using System;
using GameData.Chances;
using UnityEngine;

namespace GameData.Princesses.Appearance.Palettes
{
    [Serializable]
    public class PaletteElement<TElement> : ChanceSetup<TElement> where TElement : ScriptableObject { }
}
