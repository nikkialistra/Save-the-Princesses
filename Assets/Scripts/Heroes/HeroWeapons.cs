using Combat.Weapons.Enums;
using static Combat.Weapons.Enums.WeaponCategory;

namespace Heroes
{
    public class HeroWeapons
    {
        private WeaponType _melee;
        private WeaponType _ranged;

        public void ReplaceWeapon(WeaponType weaponType)
        {
            if (weaponType.Category() == Melee)
                ReplaceMeleeWeapon(weaponType);
            else
                ReplaceRangedWeapon(weaponType);
        }

        private WeaponType ReplaceMeleeWeapon(WeaponType weaponType)
        {
            var previous = _melee;
            _melee = weaponType;

            return previous;
        }

        private WeaponType ReplaceRangedWeapon(WeaponType weaponType)
        {
            var previous = _ranged;
            _ranged = weaponType;

            return previous;
        }
    }
}
