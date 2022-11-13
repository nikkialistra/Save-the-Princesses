using Characters;
using Heroes;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.HealthBar
{
    [RequireComponent(typeof(UIDocument))]
    [RequireComponent(typeof(HealthBarDigitsView))]
    [RequireComponent(typeof(HealthBarDecreaseAnimations))]
    [RequireComponent(typeof(HealthBarIncreaseAnimations))]
    public class HealthBarView : MonoBehaviour
    {
        public const int DefaultWidth = 50;

        private const int DefaultMaxHealth = 100;

        public float FillWidth { get; set; } = DefaultWidth;
        public bool IsFillFull => FillWidth == DefaultWidth;

        private float Health => _heroHealth.Health;
        private float MaxHealth => _heroHealth.MaxHealth;

        private VisualElement _background;
        private VisualElement _frame;

        private float _lastHealth;

        private HealthBarDigitsView _digitsView;

        private HealthBarDecreaseAnimations _decreaseAnimations;
        private HealthBarIncreaseAnimations _increaseAnimations;

        private CharacterHealth _heroHealth;

        public void Initialize(Hero hero)
        {
            BindUi();
            InitializeAnimations();
            BindHealth(hero.Health);
        }

        public void Dispose()
        {
            UnbindHealth();
        }

        private void BindUi()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;

            _background = root.Q<VisualElement>("health-bar");
            _frame = root.Q<VisualElement>("health-bar__frame");

            _digitsView = GetComponent<HealthBarDigitsView>();

            _decreaseAnimations = GetComponent<HealthBarDecreaseAnimations>();
            _increaseAnimations = GetComponent<HealthBarIncreaseAnimations>();
        }

        private void BindHealth(CharacterHealth heroHealth)
        {
            _heroHealth = heroHealth;

            _heroHealth.HealthChange += UpdateHealth;
            _heroHealth.MaxHealthChange += UpdateMaxHealth;

            UpdateMaxHealth();
            UpdateHealthValue();
        }

        private void UnbindHealth()
        {
            _heroHealth.HealthChange -= UpdateHealth;
            _heroHealth.MaxHealthChange -= UpdateMaxHealth;
        }

        private void InitializeAnimations()
        {
            _decreaseAnimations.BindUi();
            _increaseAnimations.BindUi();

            _increaseAnimations.ShowFillFullAtFullBar();
        }

        private void UpdateHealth()
        {
            if (MaxHealth == 0 || _lastHealth == Health) return;

            if (_lastHealth > Health)
                DecreaseHealth();
            else
                IncreaseHealth();

            UpdateHealthValue();
        }

        private void UpdateHealthValue()
        {
            _digitsView.UpdateForNumber(Health);

            _lastHealth = Health;
        }

        private void DecreaseHealth()
        {
            var newWidth = CalculateWidthFor(Health);

            _decreaseAnimations.DecreaseFill(newWidth);
        }

        private void IncreaseHealth()
        {
            var newWidth = CalculateWidthFor(Health);

            _increaseAnimations.IncreaseFill(newWidth);
        }

        private void UpdateMaxHealth()
        {
            var newWidth = CalculateWidthFor(MaxHealth);

            _background.style.width = newWidth;
            _frame.style.width = newWidth;

            _digitsView.UpdateWidth(newWidth);
        }

        private static int CalculateWidthFor(float health)
        {
            var healthFraction = health / DefaultMaxHealth;

            return Mathf.RoundToInt( healthFraction * DefaultWidth);
        }
    }
}
