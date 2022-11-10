using System;
using Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data.Princesses.Elements
{
    [Serializable]
    public class ElementController : ChanceSetup
    {
        [HorizontalGroup("Split", 150, MarginRight = 20)]
        [VerticalGroup("Split/Left", PaddingTop = 11)]
        [HideLabel]
        public RuntimeAnimatorController Controller;
    }
}
