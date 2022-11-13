using Princesses.Palettes.Types;
using UnityEngine;

namespace GameData.Princesses.Appearance.Palettes
{
    [CreateAssetMenu(fileName = "(Palette Name)", menuName = "GameData/Palettes/Palette")]
    public class PrincessPaletteElements : ScriptableObject
    {
        public PaletteElements<SkinPalette> SkinPaletteElements = new();
        public PaletteElements<HairPalette> HairPaletteElements = new();
        public PaletteElements<GarmentPalette> GarmentPaletteElements = new();
    }
}
