using Combat.Weapons.Enums;
using GameData.Weapons;
using UnityEngine;

namespace Combat.Weapons.Concrete
{
    public abstract class ConcreteWeapon : MonoBehaviour
    {
        public abstract WeaponType Type { get; }
        public abstract StrokeType LastStroke { get; }
        public abstract float AttackEndTime { get; set; }

        public abstract void Initialize(WeaponSpecs specs);
        public abstract bool TryStroke();
        public abstract void ResetStroke();
    }
}
