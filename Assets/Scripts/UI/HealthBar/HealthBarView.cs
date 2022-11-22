using Characters.Health;
using GameConfig.HealthBar;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.HealthBar
{
    [RequireComponent(typeof(UIDocument))]
    public class HealthBarView : MonoBehaviour
    {
        public const int DefaultWidth = 50;

        private const int DefaultMaxHealth = 100;

        public float FillWidth { get; set; } = DefaultWidth;
        public bool IsFillFull => FillWidth == DefaultWidth;

        [SerializeField] private HealthBarSprites _sprites;
        [SerializeField] private HealthBarDigitSprites _digitSprites;

        private float Health => _heroHealth.Health;
        private float MaxHealth => _heroHealth.MaxHealth;

        private VisualElement _root;

        private VisualElement _background;
        private VisualElement _frame;

        private float _lastHealth;

        private HealthBarDigits _digits;

        private HealthBarDecreaseAnimations _decreaseAnimations;
        private HealthBarIncreaseAnimations _increaseAnimations;

        private CharacterHealth _heroHealth;

        public void Initialize(CharacterHealth heroHealth)
        {
            BindUi();
            InitializeAnimations();
            BindHealth(heroHealth);
        }

        public void Dispose()
        {
            UnbindHealth();
        }

        private void BindUi()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;

            _background = _root.Q<VisualElement>("health-bar");
            _frame = _root.Q<VisualElement>("health-bar__frame");
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
            _digits = new HealthBarDigits(_root, _digitSprites);

            _decreaseAnimations = new HealthBarDecreaseAnimations(_root, _sprites, this);
            _increaseAnimations = new HealthBarIncreaseAnimations(_root, _sprites, this);

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
            _digits.UpdateForNumber(Health);

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

            _digits.UpdateWidth(newWidth);
        }

        private static int CalculateWidthFor(float health)
        {
            var healthFraction = health / DefaultMaxHealth;

            return Mathf.RoundToInt( healthFraction * DefaultWidth);
        }
    }
}
