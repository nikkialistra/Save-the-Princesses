using System.Collections;
using Heroes.Accumulations;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class GoldView : MonoBehaviour
    {
        [SerializeField] private float _timeToHide = 5f;

        private VisualElement _container;
        private Label _quantity;

        private Coroutine _hideAfterCoroutine;

        private HeroGold _heroGold;

        public void Initialize(HeroGold heroGold)
        {
            _heroGold = heroGold;

            var root = GetComponent<UIDocument>().rootVisualElement;

            _container = root.Q<VisualElement>("gold");
            _quantity = root.Q<Label>("gold__quantity");

            Hide();

            _heroGold.QuantityChanged += UpdateQuantity;
        }

        public void Dispose()
        {
            _heroGold.QuantityChanged -= UpdateQuantity;
        }

        private void UpdateQuantity(int quantity)
        {
            Show();

            _quantity.text = quantity.ToString();

            _hideAfterCoroutine = StartCoroutine(CHideAfter());
        }

        private void Show()
        {
            if (_hideAfterCoroutine != null)
            {
                StopCoroutine(_hideAfterCoroutine);
                _hideAfterCoroutine = null;
            }

            _container.RemoveFromClassList("hidden");
        }

        private IEnumerator CHideAfter()
        {
            yield return new WaitForSeconds(_timeToHide);

            Hide();
        }

        private void Hide()
        {
            _container.AddToClassList("hidden");
        }
    }
}