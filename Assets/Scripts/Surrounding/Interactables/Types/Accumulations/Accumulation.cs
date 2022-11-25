using Rooms.Items;
using UnityEngine;

namespace Surrounding.Interactables.Types.Accumulations
{
    public class Accumulation : MonoBehaviour, IItem, IInteractable
    {
        public InteractableType Type => InteractableType.Accumulation;

        [SerializeField] private AccumulationType _type;
        [SerializeField] private int _quantity;

        private bool _accumulated;

        public void SetPosition(Vector3 position, Transform parent)
        {
            transform.position = position;
            transform.parent = parent;
        }

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
