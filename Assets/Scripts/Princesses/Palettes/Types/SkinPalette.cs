using UnityEngine;

namespace Princesses.Palettes.Types
{
    [CreateAssetMenu(fileName = "(Skin Palette Name)", menuName = "Data/Palettes/Skin")]
    public class SkinPalette : ScriptableObject
    {
        public Color First;
        public Color Second;
    }
}
