using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Utilities
{
    public class SpriteTransposer : OdinEditorWindow
    {
        [PreviewField(60, ObjectFieldAlignment.Center)]
        [SerializeField] private List<Texture2D> _textures = new();

        [Space]
        [SerializeField] private int _frameWidth = 32;
        [PropertySpace(SpaceBefore = 0, SpaceAfter = 30)]
        [SerializeField] private int _frameHeight = 48;

        [MenuItem("Window/Sprite Transposer")]
        private static void OpenWindow()
        {
            GetWindow<SpriteTransposer>().Show();
        }

        [Button(ButtonSizes.Large)]
        private void Transpose()
        {
            foreach (var sprite in _textures)
                Transpose(sprite);

            _textures.Clear();
        }

        private void Transpose(Texture2D originalTexture)
        {
            CheckFrameParametersValidity();
            CheckDimensionsValidity(originalTexture);

            var cellCountX = originalTexture.width / _frameWidth;
            var cellCountY = originalTexture.height / _frameHeight;

            var newTexture = CreateTexture(cellCountX, cellCountY);

            FillTexture(newTexture, originalTexture, cellCountY, cellCountX);
            SaveTexture(newTexture, originalTexture);
        }

        private Texture2D CreateTexture(int cellCountX, int cellCountY)
        {
            var transposedWidth = _frameWidth * cellCountY;
            var transposedHeight = _frameHeight * cellCountX;

            var texture = new Texture2D(transposedWidth, transposedHeight)
            {
                filterMode = FilterMode.Point
            };

            return texture;
        }

        private void FillTexture(Texture2D @new, Texture2D original, int cellCountX, int cellCountY)
        {
            for (int cellY = 0; cellY < cellCountY; cellY++)
                for (int cellX = 0; cellX < cellCountX; cellX++)
                    FillCell(@new, original, cellX, cellY);
        }

        private void FillCell(Texture2D @new, Texture2D original, int cellX, int cellY)
        {
            for (int pixelY = 0; pixelY < _frameHeight; pixelY++)
            {
                for (int pixelX = 0; pixelX < _frameWidth; pixelX++)
                {
                    var newPosition = GetNewPosition(cellX, cellY, pixelX, pixelY);
                    var originalPosition = GetOriginalPosition(cellX, cellY, pixelX, pixelY);

                    var originalPositionFromTopY = (original.height - 1) - originalPosition.y;
                    var newPostionFromTopY = (@new.height - 1) - newPosition.y;

                    var color = original.GetPixel(originalPosition.x, originalPositionFromTopY);
                    @new.SetPixel(newPosition.x, newPostionFromTopY, color);
                }
            }
        }

        private (int x, int y) GetNewPosition(int cellX, int cellY, int pixelX, int pixelY)
        {
            var positionX = cellX * _frameWidth + pixelX;
            var positionY = cellY * _frameHeight + pixelY;

            return (positionX, positionY);
        }

        private (int x, int y) GetOriginalPosition(int cellX, int cellY, int pixelX, int pixelY)
        {
            var positionX = cellY * _frameWidth + pixelX;
            var positionY = cellX * _frameHeight + pixelY;

            return (positionX, positionY);
        }

        private static void SaveTexture(Texture2D texture, Texture2D originalTexture)
        {
            var bytes = texture.EncodeToPNG();
            var path = AssetDatabase.GetAssetPath(originalTexture);

            System.IO.File.WriteAllBytes(path, bytes);
            AssetDatabase.Refresh();
        }

        private void CheckFrameParametersValidity()
        {
            if (_frameWidth <= 1 || _frameHeight <= 1)
                throw new InvalidOperationException("Frame sizes are not correct");
        }

        private void CheckDimensionsValidity(Texture2D texture)
        {
            if (texture.width % _frameWidth != 0 || texture.height % _frameHeight != 0)
                throw new InvalidOperationException($"{texture.name} have invalid dimensions");
        }
    }
}
