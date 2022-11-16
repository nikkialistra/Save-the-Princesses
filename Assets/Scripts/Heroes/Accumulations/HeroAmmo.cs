using Sirenix.OdinInspector;
using UI;
using UnityEngine;
using Zenject;

namespace Heroes.Accumulations
{
    public class HeroAmmo : MonoBehaviour
    {
        private const int MaxQuantity = 4;

        private int _quantity;

        private AmmoView _ammoView;

        [Inject]
        public void Construct(AmmoView ammoView)
        {
            _ammoView = ammoView;
        }

        public bool TryIncrease()
        {
            if (_quantity == MaxQuantity)
                return false;

            _quantity++;
            _ammoView.UpdateQuantity(_quantity);

            return true;
        }

        public bool TrySpend()
        {
            if (_quantity == 0)
                return false;

            _quantity--;
            _ammoView.UpdateQuantity(_quantity);

            return true;
        }
    }
}
