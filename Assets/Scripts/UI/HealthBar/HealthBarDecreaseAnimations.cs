using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.HealthBar
{
    [RequireComponent(typeof(UIDocument))]
    [RequireComponent(typeof(HealthBarView))]
    public class HealthBarDecreaseAnimations : MonoBehaviour
    {
        private const float AnimationDecreaseStepLength = 0.1f;

        private const int WidthDifferenceThresholdForFullAnimation = 4;
        private const int RemainderAnimationWidthThreshold = 10;

        private const float PartWidth = 0.4f;

        [SerializeField] private Sprite _fillFull;
        [SerializeField] private Sprite _fillNotFull;
        [SerializeField] private Sprite _fillRemainderTwoPixels;
        [SerializeField] private Sprite _fillRemainderOnePixel;

        [Space]
        [SerializeField] private Sprite _fillLightStart;
        [SerializeField] private Sprite _fillLightMiddle;
        [SerializeField] private Sprite _fillLightFinish;

        [Space]
        [SerializeField] private Sprite _fillRemainderLightStart;
        [SerializeField] private Sprite _fillRemainderLightMiddle;
        [SerializeField] private Sprite _fillRemainderLightFinish;

        private VisualElement _fill;
        private VisualElement _fillWhite;

        private float _widthDifference;

        private HealthBarView _view;

        public void BindUi()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;

            _fill = root.Q<VisualElement>("health-bar__fill");
            _fillWhite = root.Q<VisualElement>("health-bar__fill-white");

            _view = GetComponent<HealthBarView>();
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
            if (_view.FillWidth > RemainderAnimationWidthThreshold)
                StartCoroutine(CShowDecreaseAnimation());
            else
                StartCoroutine(CShowRemainderDecreaseAnimation());
        }

        private IEnumerator CShowDecreaseAnimation()
        {
            PlaceFillWhite();

            if (_widthDifference > WidthDifferenceThresholdForFullAnimation)
            {
                _fill.style.backgroundImage = Background.FromSprite(_fillLightStart);

                yield return new WaitForSeconds(AnimationDecreaseStepLength);

                _fill.style.backgroundImage = Background.FromSprite(_fillLightMiddle);
                ShowPartFillWhite();

                yield return new WaitForSeconds(AnimationDecreaseStepLength);
            }

            _fill.style.backgroundImage = Background.FromSprite(_fillLightFinish);
            _fillWhite.style.width = 1;

            yield return new WaitForSeconds(AnimationDecreaseStepLength);

            UpdateFillSprite();
            HideFillWhite();
        }

        private void UpdateFillSprite()
        {
            var fillSprite = _view.FillWidth switch
            {
                HealthBarView.DefaultWidth => _fillFull,
                _ => _fillNotFull
            };

            _fill.style.backgroundImage = Background.FromSprite(fillSprite);
        }

        private IEnumerator CShowRemainderDecreaseAnimation()
        {
            PlaceFillWhite();

            SetUpRemainderPosition();

            if (_widthDifference > WidthDifferenceThresholdForFullAnimation)
            {
                _fill.style.backgroundImage = Background.FromSprite(_fillRemainderLightStart);

                yield return new WaitForSeconds(AnimationDecreaseStepLength);

                _fill.style.backgroundImage = Background.FromSprite(_fillRemainderLightMiddle);
                ShowPartFillWhite();

                yield return new WaitForSeconds(AnimationDecreaseStepLength);
            }

            _fill.style.backgroundImage = Background.FromSprite(_fillRemainderLightFinish);
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
                2 => _fillRemainderTwoPixels,
                1 => _fillRemainderOnePixel,
                _ => _fillNotFull
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
