using Data.Princesses.Appearance.Palettes;
using Princesses.Palettes.Types;
using UnityEngine;
using Zenject;

namespace Princesses.Palettes
{
    [RequireComponent(typeof(Princess))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class PrincessPaletteChoosing : MonoBehaviour
    {
        private static readonly int Original1Id = Shader.PropertyToID("_Original1");
        private static readonly int Original2Id = Shader.PropertyToID("_Original2");
        private static readonly int Original3Id = Shader.PropertyToID("_Original3");
        private static readonly int Original4Id = Shader.PropertyToID("_Original4");
        private static readonly int Original5Id = Shader.PropertyToID("_Original5");

        private static readonly int New1Id = Shader.PropertyToID("_New1");
        private static readonly int New2Id = Shader.PropertyToID("_New2");
        private static readonly int New3Id = Shader.PropertyToID("_New3");
        private static readonly int New4Id = Shader.PropertyToID("_New4");
        private static readonly int New5Id = Shader.PropertyToID("_New5");

        [SerializeField] private SpriteRenderer _bodyRenderer;
        [SerializeField] private SpriteRenderer _headRenderer;
        [SerializeField] private SpriteRenderer _garmentRenderer;
        [SerializeField] private SpriteRenderer _hairRenderer;

        private PrincessPalettesRegistry _registry;

        private Princess _princess;

        [Inject]
        public void Construct(PrincessPalettesRegistry registry)
        {
            _registry = registry;
        }

        public void Initialize()
        {
            _princess = GetComponent<Princess>();

            ApplyRandomPalette();
        }

        private void ApplyRandomPalette()
        {
            var type = _princess.Type;

            var skinPalette = _registry.GetRandomSkinPalette(type);
            var hairPalette = _registry.GetRandomHairPalette(type);
            var garmentPalette = _registry.GetRandomGarmentPalette(type);

            ReplaceColors(_bodyRenderer.materials[0], skinPalette, _registry.OriginalSkinPalette);
            ReplaceColors(_headRenderer.materials[0], skinPalette, _registry.OriginalSkinPalette,
                garmentPalette, _registry.OriginalGarmentPalette);
            ReplaceColors(_hairRenderer.materials[0], hairPalette, _registry.OriginalHairPalette);
            ReplaceColors(_garmentRenderer.materials[0], garmentPalette, _registry.OriginalGarmentPalette);
        }

        private static void ReplaceColors(Material material, SkinPalette @new, SkinPalette original)
        {
            material.SetColor(Original1Id, original.First);
            material.SetColor(Original2Id, original.Second);

            material.SetColor(New1Id, @new.First);
            material.SetColor(New2Id, @new.Second);
        }

        private void ReplaceColors(Material material,
            SkinPalette firstPaletteNew, SkinPalette firstPaletteOriginal,
            GarmentPalette secondPaletteNew, GarmentPalette secondPaletteOriginal)
        {
            material.SetColor(Original1Id, firstPaletteOriginal.First);
            material.SetColor(Original2Id, firstPaletteOriginal.Second);
            material.SetColor(Original3Id, secondPaletteOriginal.First);
            material.SetColor(Original4Id, secondPaletteOriginal.Second);
            material.SetColor(Original5Id, secondPaletteOriginal.Third);

            material.SetColor(New1Id, firstPaletteNew.First);
            material.SetColor(New2Id, firstPaletteNew.Second);
            material.SetColor(New3Id, secondPaletteNew.First);
            material.SetColor(New4Id, secondPaletteNew.Second);
            material.SetColor(New5Id, secondPaletteNew.Third);
        }

        private static void ReplaceColors(Material material, HairPalette @new, HairPalette original)
        {
            material.SetColor(Original1Id, original.First);
            material.SetColor(Original2Id, original.Second);
            material.SetColor(Original3Id, original.Third);
            material.SetColor(Original4Id, original.Fourth);

            material.SetColor(New1Id, @new.First);
            material.SetColor(New2Id, @new.Second);
            material.SetColor(New3Id, @new.Third);
            material.SetColor(New4Id, @new.Fourth);
        }

        private static void ReplaceColors(Material material, GarmentPalette @new, GarmentPalette original)
        {
            material.SetColor(Original1Id, original.First);
            material.SetColor(Original2Id, original.Second);
            material.SetColor(Original3Id, original.Third);

            material.SetColor(New1Id, @new.First);
            material.SetColor(New2Id, @new.Second);
            material.SetColor(New3Id, @new.Third);
        }
    }
}
