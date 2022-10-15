using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.HealthBar
{
    [RequireComponent(typeof(UIDocument))]
    public class HealthBarDigitsView : MonoBehaviour
    {
        [SerializeField] private List<Sprite> _digitSprites;

        private VisualElement _digits;

        private Image _digit1;
        private Image _digit2;
        private Image _digit3;

        public void Initialize()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;

            _digits = root.Q<VisualElement>("health-bar__digits");

            _digit1 = root.Q<Image>("digit1");
            _digit2 = root.Q<Image>("digit2");
            _digit3 = root.Q<Image>("digit3");
        }

        public void UpdateForNumber(float number)
        {
            switch (number)
            {
                case >= 100:
                    SetThreeDigits(number);
                    break;
                case >= 10:
                    SetTwoDigits(number);
                    break;
                default:
                    SetOneDigit(number);
                    break;
            }
        }

        public void UpdateWidth(int newWidth)
        {
            _digits.style.width = newWidth;
        }

        private void SetThreeDigits(float value)
        {
            _digit1.sprite = GetSpriteFor(value / 100);
            _digit2.sprite = GetSpriteFor(value / 10 % 10);
            _digit3.sprite = GetSpriteFor(value % 10);

            _digit1.style.display = DisplayStyle.Flex;
            _digit2.style.display = DisplayStyle.Flex;
            _digit3.style.display = DisplayStyle.Flex;
        }

        private void SetTwoDigits(float value)
        {
            _digit2.sprite = GetSpriteFor(value / 10);
            _digit3.sprite = GetSpriteFor(value % 10);

            _digit1.style.display = DisplayStyle.None;
            _digit2.style.display = DisplayStyle.Flex;
            _digit3.style.display = DisplayStyle.Flex;
        }

        private void SetOneDigit(float value)
        {
            _digit3.sprite = GetSpriteFor(value % 10);

            _digit1.style.display = DisplayStyle.None;
            _digit2.style.display = DisplayStyle.None;
            _digit3.style.display = DisplayStyle.Flex;
        }

        private Sprite GetSpriteFor(float digit)
        {
            var index = (int)digit;

            if (index is < 0 or > 9)
                throw new ArgumentException("Index should be between 0 and 9");

            return _digitSprites[index];
        }
    }
}
