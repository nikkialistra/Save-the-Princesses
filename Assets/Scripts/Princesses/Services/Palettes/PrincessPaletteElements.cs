using System;
using Princesses.Palettes.Types;

namespace Princesses.Services.Palettes
{
    [Serializable]
    public class PrincessPaletteElements
    {
        public PaletteElements<SkinPalette> SkinPaletteElements = new();
        public PaletteElements<HairPalette> HairPaletteElements = new();
        public PaletteElements<GarmentPalette> GarmentPaletteElements = new();
    }
}
