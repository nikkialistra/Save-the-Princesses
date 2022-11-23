using System;
using static Combat.Weapons.Enums.WeaponCategory;

namespace Combat.Weapons.Enums
{
    public static class WeaponTypeExtensions
    {
        public static WeaponCategory Category(this WeaponType weaponType)
        {
            return weaponType switch
            {
                WeaponType.NoWeapon => Melee,
                WeaponType.Sword => Melee,
                WeaponType.Fork => Melee,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
