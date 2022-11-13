using System;
using GameData.Chances;
using UnityEngine;

namespace GameData.Princesses.Appearance.Palettes
{
    [Serializable]
    public class PaletteElements<TElement> : ChanceList<PaletteElement<TElement>, TElement> where TElement : ScriptableObject { }
}
