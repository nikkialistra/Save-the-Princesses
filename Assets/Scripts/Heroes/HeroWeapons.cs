using System;
using Combat.Weapons.Enums;
using static Combat.Weapons.Enums.WeaponCategory;

namespace Heroes
{
    public class HeroWeapons
    {
        public event Action<WeaponType> WeaponChanged;

        private WeaponCategory _activeCategory = Melee;

        private WeaponType _melee;
        private WeaponType _ranged;

        private readonly HeroInput _input;

        public HeroWeapons(HeroInput input)
        {
            _input = input;

            _input.SwapWeapons += TrySwapWeapons;
        }

        public void Dispose()
        {
            _input.SwapWeapons -= TrySwapWeapons;
        }

        public WeaponType TryReplaceWeapon(WeaponType weaponType)
        {
            return weaponType.Category() == Melee
                ? TryReplaceMeleeWeapon(weaponType)
                : TryReplaceRangedWeapon(weaponType);
        }

        private void TrySwapWeapons()
        {
            if (_activeCategory == Melee)
                TrySwapToRanged();
            else
                TrySwapToMelee();
        }

        private WeaponType TryReplaceMeleeWeapon(WeaponType weaponType)
        {
            if (_melee == weaponType) return WeaponType.NoWeapon;

            var previous = _melee;
            _melee = weaponType;

            if (_activeCategory == Melee)
                WeaponChanged?.Invoke(_melee);

            return previous;
        }

        private WeaponType TryReplaceRangedWeapon(WeaponType weaponType)
        {
            if (_ranged == weaponType) return WeaponType.NoWeapon;

            var previous = _ranged;
            _ranged = weaponType;

            if (_activeCategory == Ranged)
                WeaponChanged?.Invoke(_ranged);

            return previous;
        }

        private void TrySwapToMelee()
        {
            if (_melee == WeaponType.NoWeapon) return;

            _activeCategory = Melee;

            WeaponChanged?.Invoke(_melee);
        }

        private void TrySwapToRanged()
        {
            if (_ranged == WeaponType.NoWeapon) return;

            _activeCategory = Ranged;

            WeaponChanged?.Invoke(_ranged);
        }
    }
}
