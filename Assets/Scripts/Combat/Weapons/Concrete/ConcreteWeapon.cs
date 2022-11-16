using Combat.Attacks;
using GameData.Weapons;
using UnityEngine;

namespace Combat.Weapons.Concrete
{
    public abstract class ConcreteWeapon : MonoBehaviour
    {
        public abstract bool TryStroke();
        public abstract void ResetStroke();

        public abstract WeaponSpecs Specs { get; }
        public abstract StrokeType LastStroke { get; }

        public Attack Attack { get; set; }
    }
}
