using System;
using System.Collections.Generic;
using System.Linq;
using Data.Chances;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Data.Princesses.Palettes
{
    [Serializable]
    public class PaletteElements<TElement> : ChanceList<PaletteElement<TElement>, TElement> where TElement : ScriptableObject { }
}
