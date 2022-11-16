using GameData.Weapons;
using UnityEngine;

namespace Combat.Weapons.Concrete
{
    public abstract class ConcreteWeapon : MonoBehaviour
    {
        public abstract void Initialize(WeaponSpecs specs);
        public abstract bool TryStroke();
        public abstract void ResetStroke();

        public abstract StrokeType LastStroke { get; }
    }
}
