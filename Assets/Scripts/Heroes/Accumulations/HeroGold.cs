using UI;
using UnityEngine;
using Zenject;

namespace Heroes.Accumulations
{
    public class HeroGold : MonoBehaviour
    {
        private int _quantity;

        private GoldView _goldView;

        [Inject]
        public void Construct(GoldView goldView)
        {
            _goldView = goldView;
        }

        public void Add(int value)
        {
            _quantity += value;

            _goldView.UpdateQuantity(_quantity);
        }

        public void Remove(int value)
        {
            _quantity -= value;

            _goldView.UpdateQuantity(_quantity);
        }
    }
}
