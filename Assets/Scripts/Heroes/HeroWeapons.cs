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

        private Weapon Active
        {
            get => _active;
            set
            {
                _active = value;
                ActiveWeaponChanged?.Invoke(_active);
            }
        }

        private Weapon _active;

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

        public bool TryMeleeStroke()
        {
            if (!HasMelee) return false;

            if (Melee.TryStroke())
            {
                Active = Melee;
                Melee.Attack.Do(Melee.LastStroke);
                return true;
            }

            return false;
        }

        public bool TryRangedStroke()
        {
            if (!HasRanged) return false;

            if (Ranged.TryStroke())
            {
                Active = Ranged;
                Ranged.Attack.Do(Ranged.LastStroke);
                return true;
            }

            return false;
        }

        public void UpdateRotation(float direction)
        {
            if (Active != null)
                Active.Attack.UpdateRotation(direction);
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

            UpdateActiveIf(MeleeCategory);
        }

        private void UpdateRangedWeapon(WeaponType weaponType)
        {
            Ranged.Dispose();
            Ranged = _weaponFactory.Create(weaponType, _parent);

            UpdateActiveIf(RangedCategory);
        }

        private void UpdateActiveIf(WeaponCategory category)
        {
            if (_active.Type.Category() != category) return;

            Active = category == MeleeCategory
                ? Melee
                : Ranged;
        }
    }
}
