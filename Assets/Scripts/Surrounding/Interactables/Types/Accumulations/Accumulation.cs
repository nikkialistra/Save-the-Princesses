using UnityEngine;

namespace Surrounding.Interactables.Types.Accumulations
{
    public class Accumulation : MonoBehaviour, IInteractable
    {
        public InteractableType Type => InteractableType.Accumulation;

        [SerializeField] private AccumulationType _type;
        [SerializeField] private int _quantity;

        private bool _accumulated;

        public Accumulated TryPickup()
        {
            if (_accumulated) return null;

            var accumulated = new Accumulated
            {
                Type = _type,
                Quantity = _quantity
            };

            return accumulated;
        }

        public void MarkPickuped()
        {
            _accumulated = true;

            Destroy(gameObject);
        }
    }
}
