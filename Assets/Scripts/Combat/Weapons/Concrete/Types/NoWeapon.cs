using Combat.Weapons.Enums;
using GameData.Weapons;
using UnityEngine;
using static Combat.StrokeType;

namespace Combat.Weapons.Concrete.Types
{
    public class NoWeapon : ConcreteWeapon
    {
        public override WeaponType Type => WeaponType.NoWeapon;
        public override StrokeType LastStroke => First;
        public override float AttackEndTime
        {
            get => Mathf.Infinity;
            set { }
        }

        public override void Initialize(WeaponSpecs specs) { }

        public override bool TryStroke()
        {
            return false;
        }

        public override void ResetStroke() { }
    }
}
