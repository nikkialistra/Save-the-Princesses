using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.HealthBar
{
    [RequireComponent(typeof(UIDocument))]
    [RequireComponent(typeof(HealthBarView))]
    public class HealthBarIncreaseAnimations : MonoBehaviour
    {
        private const float AnimationIncreaseStepLength = 0.1f;

        private const int WidthDifferenceThresholdForFullAnimation = 4;

        private const float PartWidth = 0.4f;

        [SerializeField] private Sprite _fillFull;
        [SerializeField] private Sprite _fillNotFull;
        [SerializeField] private Sprite _fillRemainderTwoPixels;
        [SerializeField] private Sprite _fillRemainderOnePixel;

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

        public void IncreaseFill(float newWidth)
        {
            if (IsWidthNotChanged(newWidth)) return;

            ValidateIncreaseOperation(newWidth);

            _fill.style.width = newWidth;
            _widthDifference =  newWidth - _view.FillWidth;
            _view.FillWidth = newWidth;

            StartCoroutine(CShowIncreaseAnimation());
        }

        public void ShowFillFullAtFullBar()
        {
            if (_view.IsFillFull)
            {
                _fill.style.width = _view.FillWidth;
                _fill.style.backgroundImage = Background.FromSprite(_fillFull);
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
                HealthBarView.DefaultWidth => _fillFull,
                2 => _fillRemainderTwoPixels,
                1 => _fillRemainderOnePixel,
                _ => _fillNotFull
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
