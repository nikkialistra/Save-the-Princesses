using Combat.Weapons.Enums;
using UnityEngine;

namespace Surrounding.Interactables.Types
{
    public class Weaponry : MonoBehaviour, IInteractable
    {
        public InteractableType Type => InteractableType.Weaponry;

        public WeaponType WeaponType => _weaponType;

        [SerializeField] private WeaponType _weaponType;
    }
}
