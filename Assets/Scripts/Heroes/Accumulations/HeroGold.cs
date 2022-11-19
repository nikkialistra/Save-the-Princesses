using System;

namespace Heroes.Accumulations
{
    public class HeroGold
    {
        public event Action<int> QuantityChanged;

        private int _quantity;

        public void Add(int value)
        {
            _quantity += value;

            QuantityChanged?.Invoke(_quantity);
        }

        public void Remove(int value)
        {
            _quantity -= value;

            QuantityChanged?.Invoke(_quantity);
        }
    }
}
