using UnityEngine;

namespace Princesses.Palettes.Types
{
    [CreateAssetMenu(fileName = "(Garment Palette Name)", menuName = "GameData/Palettes/Garment")]
    public class GarmentPalette : ScriptableObject
    {
        public Color First;
        public Color Second;
        public Color Third;
    }
}
