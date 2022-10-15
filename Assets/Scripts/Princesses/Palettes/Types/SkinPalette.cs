using UnityEngine;

namespace Princesses.Palettes.Types
{
    [CreateAssetMenu(fileName = "Skin Palette", menuName = "Palettes/Skin")]
    public class SkinPalette : ScriptableObject
    {
        public Color First;
        public Color Second;
    }
}
