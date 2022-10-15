using Combat.Attacks;
using UnityEngine;

namespace Combat.Weapons
{
    public abstract class ConcreteWeapon : MonoBehaviour
    {
        public abstract WeaponSpecs Specs { get; }
        public abstract StrokeType LastStroke { get; }

        public abstract bool TryStroke();
        public abstract void ResetStroke();
    }
}
