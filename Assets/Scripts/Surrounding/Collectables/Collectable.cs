using UnityEngine;

namespace Surrounding.Collectables
{
    public class Collectable : MonoBehaviour
    {
        [SerializeField] private CollectableType _type;
        [SerializeField] private int _quantity;

        private bool _pickuped;

        public Collected TryPickup()
        {
            if (_pickuped) return null;

            var collected = new Collected()
            {
                Type = _type,
                Quantity = _quantity
            };

            return collected;
        }

        public void MarkPickuped()
        {
            _pickuped = true;

            Destroy(gameObject);
        }
    }
}
