using Combat.Weapons.Enums;
using Rooms.Items;
using UnityEngine;

namespace Surrounding.Interactables.Types
{
    public class WeaponObject : MonoBehaviour, IItem, IInteractable
    {
        public InteractableType Type => InteractableType.Weapon;

        public WeaponType WeaponType => _weaponType;

        [SerializeField] private WeaponType _weaponType;

        public void SetPosition(Vector3 position, Transform parent)
        {
            transform.position = position;
            transform.parent = parent;
        }
    }
}
