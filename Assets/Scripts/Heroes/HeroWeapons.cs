using System;
using Characters;
using Combat.Weapons;
using Combat.Weapons.Enums;
using Combat.Weapons.Services;
using static Combat.Weapons.Enums.WeaponCategory;

namespace Heroes
{
    public class HeroWeapons
    {
        public event Action<Weapon> ActiveWeaponChanged;

        public bool HasMelee => Melee != null;
        public bool HasRanged => Ranged != null;

        public Weapon Melee { get; private set; }
        public Weapon Ranged { get; private set; }

        private WeaponCategory _activeCategory = MeleeCategory;

        private readonly Character _parent;

        private readonly WeaponFactory _weaponFactory;

        public HeroWeapons(Character parent, WeaponFactory weaponFactory)
        {
            _parent = parent;
            _weaponFactory = weaponFactory;
        }

        public void Tick()
        {
            if (Melee != null)
                Melee.Tick();

            if (Ranged != null)
                Ranged.Tick();
        }

        public WeaponType TryReplaceWeapon(WeaponType weaponType)
        {
            return weaponType.Category() == MeleeCategory
                ? TryReplaceMeleeWeapon(weaponType)
                : TryReplaceRangedWeapon(weaponType);
        }

        private void TrySwapWeapons()
        {
            if (_activeCategory == MeleeCategory)
                TrySwapToRanged();
            else
                TrySwapToMelee();
        }

        private WeaponType TryReplaceMeleeWeapon(WeaponType weaponType)
        {
            if (Melee.Type == weaponType) return WeaponType.NoWeapon;

            var previous = Melee.Type;
            UpdateMeleeWeapon(weaponType);

            return previous;
        }

        private WeaponType TryReplaceRangedWeapon(WeaponType weaponType)
        {
            if (Ranged.Type == weaponType) return WeaponType.NoWeapon;

            var previous = Ranged.Type;
            UpdateRangedWeapon(weaponType);

            return previous;
        }

        private void UpdateMeleeWeapon(WeaponType weaponType)
        {
            Melee.Dispose();

            Melee = _weaponFactory.Create(weaponType, _parent);

            if (_activeCategory == MeleeCategory)
                ActiveWeaponChanged?.Invoke(Melee);
        }

        private void UpdateRangedWeapon(WeaponType weaponType)
        {
            Ranged.Dispose();

            Ranged = _weaponFactory.Create(weaponType, _parent);

            if (_activeCategory == MeleeCategory)
                ActiveWeaponChanged?.Invoke(Ranged);
        }

        private void TrySwapToMelee()
        {
            if (Melee.Type == WeaponType.NoWeapon) return;

            _activeCategory = MeleeCategory;

            ActiveWeaponChanged?.Invoke(Melee);
        }

        private void TrySwapToRanged()
        {
            if (Ranged.Type == WeaponType.NoWeapon) return;

            _activeCategory = RangedCategory;

            ActiveWeaponChanged?.Invoke(Ranged);
        }
    }
}
