using System;
using System.Collections;
using GameConfig.HealthBar;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.HealthBar
{
    public class HealthBarDecreaseAnimations
    {
        private const float AnimationDecreaseStepLength = 0.1f;

        private const int WidthDifferenceThresholdForFullAnimation = 4;
        private const int RemainderAnimationWidthThreshold = 10;

        private const float PartWidth = 0.4f;

        private HealthBarSprites _sprites;

        private readonly VisualElement _fill;
        private readonly VisualElement _fillWhite;

        private float _widthDifference;

        private readonly HealthBarView _view;

        public HealthBarDecreaseAnimations(VisualElement root, HealthBarSprites sprites, HealthBarView view)
        {
            _sprites = sprites;
            _view = view;

            _fill = root.Q<VisualElement>("health-bar__fill");
            _fillWhite = root.Q<VisualElement>("health-bar__fill-white");
        }

        public void DecreaseFill(int newWidth)
        {
            if (IsWidthNotChanged(newWidth)) return;

            ValidateDecreaseOperation(newWidth);

            _fill.style.width = newWidth;
            _widthDifference = _view.FillWidth - newWidth;
            _view.FillWidth = newWidth;

            ChooseDecreaseAnimation();
        }

        private void ChooseDecreaseAnimation()
        {
            _view.StartCoroutine(_view.FillWidth > RemainderAnimationWidthThreshold
                ? CShowDecreaseAnimation()
                : CShowRemainderDecreaseAnimation());
        }

        private IEnumerator CShowDecreaseAnimation()
        {
            PlaceFillWhite();

            if (_widthDifference > WidthDifferenceThresholdForFullAnimation)
            {
                _fill.style.backgroundImage = Background.FromSprite(_sprites.LightStart);

                yield return new WaitForSeconds(AnimationDecreaseStepLength);

                _fill.style.backgroundImage = Background.FromSprite(_sprites.LightMiddle);
                ShowPartFillWhite();

                yield return new WaitForSeconds(AnimationDecreaseStepLength);
            }

            _fill.style.backgroundImage = Background.FromSprite(_sprites.LightFinish);
            _fillWhite.style.width = 1;

            yield return new WaitForSeconds(AnimationDecreaseStepLength);

            UpdateFillSprite();
            HideFillWhite();
        }

        private void UpdateFillSprite()
        {
            var fillSprite = _view.FillWidth switch
            {
                HealthBarView.DefaultWidth => _sprites.Full,
                _ => _sprites.NotFull
            };

            _fill.style.backgroundImage = Background.FromSprite(fillSprite);
        }

        private IEnumerator CShowRemainderDecreaseAnimation()
        {
            PlaceFillWhite();

            SetUpRemainderPosition();

            if (_widthDifference > WidthDifferenceThresholdForFullAnimation)
            {
                _fill.style.backgroundImage = Background.FromSprite(_sprites.RemainderLightStart);

                yield return new WaitForSeconds(AnimationDecreaseStepLength);

                _fill.style.backgroundImage = Background.FromSprite(_sprites.RemainderLightMiddle);
                ShowPartFillWhite();

                yield return new WaitForSeconds(AnimationDecreaseStepLength);
            }

            _fill.style.backgroundImage = Background.FromSprite(_sprites.RemainderLightFinish);
            _fillWhite.style.width = 1;

            yield return new WaitForSeconds(AnimationDecreaseStepLength);

            ResetRemainderPosition();
            ChooseRemainderSprite();

            HideFillWhite();
        }

        private void PlaceFillWhite()
        {
            _fillWhite.style.marginLeft = _view.FillWidth;
            _fillWhite.style.width = _widthDifference;

            _fillWhite.RemoveFromClassList("hidden");
        }

        private void ShowPartFillWhite()
        {
            _fillWhite.style.width = (int)(_widthDifference * PartWidth);
        }

        private void HideFillWhite()
        {
            _fillWhite.AddToClassList("hidden");
        }

        private void SetUpRemainderPosition()
        {
            var thresholdDifference = RemainderAnimationWidthThreshold - _view.FillWidth;

            _fill.style.marginLeft = -1 * thresholdDifference;
            _fill.style.width = _view.FillWidth + thresholdDifference;
        }

        private void ResetRemainderPosition()
        {
            _fill.style.marginLeft = 0;
            _fill.style.width = _view.FillWidth;
        }

        private void ChooseRemainderSprite()
        {
            var remainderSprite = _view.FillWidth switch
            {
                2 => _sprites.RemainderTwoPixels,
                1 => _sprites.RemainderOnePixel,
                _ => _sprites.NotFull
            };

            _fill.style.backgroundImage = Background.FromSprite(remainderSprite);
        }

        private bool IsWidthNotChanged(float newWidth)
        {
            return _view.FillWidth == newWidth;
        }

        private void ValidateDecreaseOperation(float newWidth)
        {
            if (newWidth > _view.FillWidth)
                throw new InvalidOperationException(
                    $"Decrease animation should shorten fill width: trying change {_view.FillWidth} to {newWidth}");
        }
    }
}
