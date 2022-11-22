using System;
using System.Collections;
using GameConfig.HealthBar;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.HealthBar
{
    public class HealthBarIncreaseAnimations
    {
        private const float AnimationIncreaseStepLength = 0.1f;

        private const int WidthDifferenceThresholdForFullAnimation = 4;

        private const float PartWidth = 0.4f;

        private HealthBarSprites _sprites;

        private readonly VisualElement _fill;
        private readonly VisualElement _fillWhite;

        private float _widthDifference;

        private readonly HealthBarView _view;

        public HealthBarIncreaseAnimations(VisualElement root, HealthBarSprites sprites, HealthBarView view)
        {
            _sprites = sprites;
            _view = view;

            _fill = root.Q<VisualElement>("health-bar__fill");
            _fillWhite = root.Q<VisualElement>("health-bar__fill-white");
        }

        public void IncreaseFill(float newWidth)
        {
            if (IsWidthNotChanged(newWidth)) return;

            ValidateIncreaseOperation(newWidth);

            _fill.style.width = newWidth;
            _widthDifference =  newWidth - _view.FillWidth;
            _view.FillWidth = newWidth;

            _view.StartCoroutine(CShowIncreaseAnimation());
        }

        public void ShowFillFullAtFullBar()
        {
            if (_view.IsFillFull)
            {
                _fill.style.width = _view.FillWidth;
                _fill.style.backgroundImage = Background.FromSprite(_sprites.Full);
            }
        }

        private IEnumerator CShowIncreaseAnimation()
        {
            ChooseFillSprite();
            PlaceFillWhite();

            yield return new WaitForSeconds(AnimationIncreaseStepLength);

            if (_widthDifference > WidthDifferenceThresholdForFullAnimation)
            {
                ShowPartFillWhite();

                yield return new WaitForSeconds(AnimationIncreaseStepLength);
            }

            _fillWhite.style.width = 1;

            yield return new WaitForSeconds(AnimationIncreaseStepLength);

            HideFillWhite();
        }

        private void ChooseFillSprite()
        {
            var fillSprite = _view.FillWidth switch
            {
                HealthBarView.DefaultWidth => _sprites.Full,
                2 => _sprites.RemainderTwoPixels,
                1 => _sprites.RemainderOnePixel,
                _ => _sprites.NotFull
            };

            _fill.style.backgroundImage = Background.FromSprite(fillSprite);
        }

        private void ShowPartFillWhite()
        {
            var partWidth = (int)(_widthDifference * PartWidth);
            var offset = _widthDifference - partWidth;

            _fillWhite.style.marginLeft = _view.FillWidth - _widthDifference + offset;
            _fillWhite.style.width = (int)(_widthDifference * PartWidth);
        }

        private void PlaceFillWhite()
        {
            _fillWhite.style.marginLeft = _view.FillWidth - _widthDifference;
            _fillWhite.style.width = _widthDifference;

            _fillWhite.RemoveFromClassList("hidden");
        }

        private void HideFillWhite()
        {
            _fillWhite.AddToClassList("hidden");
        }

        private bool IsWidthNotChanged(float newWidth)
        {
            return _view.FillWidth == newWidth;
        }

        private void ValidateIncreaseOperation(float newWidth)
        {
            if (newWidth < _view.FillWidth)
                throw new InvalidOperationException(
                    $"Increase animation should lengthen fill width: trying change {_view.FillWidth} to {newWidth}");
        }
    }
}
