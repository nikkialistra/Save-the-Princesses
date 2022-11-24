using Combat.Weapons.Enums;
using UnityEngine;

namespace Surrounding.Interactables.Types
{
    public class WeaponObject : MonoBehaviour, IInteractable
    {
        public InteractableType Type => InteractableType.Weapon;

        public WeaponType WeaponType => _weaponType;

        [SerializeField] private WeaponType _weaponType;
    }
}
