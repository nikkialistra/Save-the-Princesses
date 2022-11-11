using System.Collections.Generic;
using Princesses.Palettes.Types;
using Princesses.Types;
using Sirenix.OdinInspector;
using UnityEngine;
using static Princesses.Types.PrincessType;

namespace Data.Princesses.Palettes
{
    public class PrincessPalettesRegistry : SerializedMonoBehaviour
    {
        public SkinPalette OriginalSkinPalette => _originalSkinPalette;
        public HairPalette OriginalHairPalette => _originalHairPalette;
        public GarmentPalette OriginalGarmentPalette => _originalGarmentPalette;

        [SerializeField] private SkinPalette _originalSkinPalette;
        [SerializeField] private HairPalette _originalHairPalette;
        [SerializeField] private GarmentPalette _originalGarmentPalette;

        [SerializeField] private PrincessPaletteElements _defaultPaletteElements;

        [Space]
        [SerializeField] private Dictionary<PrincessType, PrincessPaletteElements> _customPaletteElementsMap = new();

        [Button(ButtonSizes.Medium), GUIColor(0.4f, 0.8f, 1)]
        private void AddNewElement()
        {
            _customPaletteElementsMap.Add(Standard, ScriptableObject.CreateInstance<PrincessPaletteElements>());
        }

        public SkinPalette GetRandomSkinPalette(PrincessType princessType)
        {
            if (princessType == Standard)
                return GetDefaultSkinRandom();

            if (_customPaletteElementsMap.ContainsKey(princessType) == false)
                return GetDefaultSkinRandom();

            var customSkinPalettes = _customPaletteElementsMap[princessType].SkinPaletteElements;

            return customSkinPalettes.Empty ? GetDefaultSkinRandom() : customSkinPalettes.GetRandom();
        }

        public HairPalette GetRandomHairPalette(PrincessType princessType)
        {
            if (princessType == Standard)
                return GetDefaultHairRandom();

            if (_customPaletteElementsMap.ContainsKey(princessType) == false)
                return GetDefaultHairRandom();

            var customHairPalettes = _customPaletteElementsMap[princessType].HairPaletteElements;

            return customHairPalettes.Empty ? GetDefaultHairRandom() : customHairPalettes.GetRandom();
        }

        public GarmentPalette GetRandomGarmentPalette(PrincessType princessType)
        {
            if (princessType == Standard)
                return GetDefaultGarmentRandom();

            if (_customPaletteElementsMap.ContainsKey(princessType) == false)
                return GetDefaultGarmentRandom();

            var customGarmentPalettes = _customPaletteElementsMap[princessType].GarmentPaletteElements;

            return customGarmentPalettes.Empty ? GetDefaultGarmentRandom() : customGarmentPalettes.GetRandom();
        }

        private SkinPalette GetDefaultSkinRandom()
        {
            return _defaultPaletteElements.SkinPaletteElements.GetRandom();
        }

        private HairPalette GetDefaultHairRandom()
        {
            return _defaultPaletteElements.HairPaletteElements.GetRandom();
        }

        private GarmentPalette GetDefaultGarmentRandom()
        {
            return _defaultPaletteElements.GarmentPaletteElements.GetRandom();
        }
    }
}
