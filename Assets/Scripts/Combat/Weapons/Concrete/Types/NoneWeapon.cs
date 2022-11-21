using GameData.Weapons;
using static Combat.StrokeType;

namespace Combat.Weapons.Concrete.Types
{
    public class NoneWeapon : ConcreteWeapon
    {
        public override StrokeType LastStroke => First;

        public override void Initialize(WeaponSpecs specs) { }

        public override bool TryStroke()
        {
            return false;
        }

        public override void ResetStroke() { }
    }
}
