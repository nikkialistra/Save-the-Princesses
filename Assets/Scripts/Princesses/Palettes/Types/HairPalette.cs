using UnityEngine;

namespace Princesses.Palettes.Types
{
    [CreateAssetMenu(fileName = "(Hair Palette Name)", menuName = "Data/Palettes/Hair")]
    public class HairPalette : ScriptableObject
    {
        public Color First;
        public Color Second;
        public Color Third;
        public Color Fourth;
    }
}
