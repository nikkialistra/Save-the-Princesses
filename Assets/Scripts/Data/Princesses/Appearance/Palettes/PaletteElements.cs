using System;
using Data.Chances;
using UnityEngine;

namespace Data.Princesses.Appearance.Palettes
{
    [Serializable]
    public class PaletteElements<TElement> : ChanceList<PaletteElement<TElement>, TElement> where TElement : ScriptableObject { }
}
