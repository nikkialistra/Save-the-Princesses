using System;
using Data.Chances;
using UnityEngine;

namespace Data.Princesses.Appearance.Palettes
{
    [Serializable]
    public class PaletteElement<TElement> : ChanceSetup<TElement> where TElement : ScriptableObject { }
}
