using StatSystem;
using UnityEngine.UIElements;

namespace UI.Stats
{
    public class StatView
    {
        public bool HideWhenZero { private get; set; }

        private readonly VisualElement _container;
        private readonly Label _value;
        private readonly Label _modifier;

        private IStat _stat;

        public StatView(VisualElement container, Label value, Label modifier)
        {
            _container = container;
            _value = value;
            _modifier = modifier;
        }

        public void BindStat(IStat stat)
        {
            _stat = stat;

            _stat.ValueChange += Update;

            Update(stat.Value);
        }

        public void UnbindStat()
        {
            _stat.ValueChange -= Update;
        }

        private void Update(float value)
        {
            if (HideWhenZero && value == 0)
                Hide();
            else
                Show();

            _value.text = $"{value}";

            UpdateModifier();
        }

        private void UpdateModifier()
        {
            var difference = _stat.Value - _stat.BaseValue;

            if (difference != 0)
                ShowModifier(difference);
            else
                HideModifier();
        }

        private void Show()
        {
            _container.RemoveFromClassList("hidden");
        }

        private void Hide()
        {
            _container.AddToClassList("hidden");
        }

        private void ShowModifier(float difference)
        {
            _modifier.RemoveFromClassList("hidden");

            if (difference > 0)
                ShowPositiveModifier(difference);
            else
                ShowNegativeModifier(difference);
        }

        private void HideModifier()
        {
            _modifier.AddToClassList("hidden");
        }

        private void ShowPositiveModifier(float value)
        {
            _modifier.text = $"+{value}";
            _modifier.AddToClassList("modifier-positive");
            _modifier.RemoveFromClassList("modifier-negative");
        }

        private void ShowNegativeModifier(float value)
        {
            _modifier.text = $"{value}";
            _modifier.AddToClassList("modifier-negative");
            _modifier.RemoveFromClassList("modifier-positive");
        }
    }
}
