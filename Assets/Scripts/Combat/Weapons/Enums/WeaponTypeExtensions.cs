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
                WeaponType.NoWeapon => MeleeCategory,
                WeaponType.Sword => MeleeCategory,
                WeaponType.Fork => MeleeCategory,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
