using System;

namespace Heroes.Accumulations
{
    public class HeroAmmo
    {
        private const int MaxQuantity = 4;

        public event Action<int> QuantityChanged;

        private int _quantity;

        public bool TryIncrease(int value)
        {
            if (_quantity + value > MaxQuantity)
                return false;

            _quantity += value;
            QuantityChanged?.Invoke(_quantity);

            return true;
        }

        public bool TrySpend()
        {
            if (_quantity == 0)
                return false;

            _quantity--;
            QuantityChanged?.Invoke(_quantity);

            return true;
        }
    }
}
