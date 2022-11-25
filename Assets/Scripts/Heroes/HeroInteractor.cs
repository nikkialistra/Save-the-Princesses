using Combat.Weapons.Enums;
using Combat.Weapons.Services;
using Heroes.Accumulations;
using Surrounding.Interactables;
using Surrounding.Interactables.Types;
using Surrounding.Interactables.Types.Accumulations;

namespace Heroes
{
    public class HeroInteractor
    {
        private Hero _hero;
        private readonly HeroAccumulations _accumulations;
        private readonly HeroWeapons _weapons;
        private readonly WeaponFactory _weaponFactory;

        public HeroInteractor(Hero hero, HeroAccumulations accumulations, HeroWeapons weapons, WeaponFactory weaponFactory)
        {
            _hero = hero;
            _accumulations = accumulations;
            _weapons = weapons;
            _weaponFactory = weaponFactory;
        }

        public void Do(IInteractable interactable)
        {
            switch (interactable.Type)
            {
                case InteractableType.Accumulation:
                    Accumulate((Accumulation)interactable);
                    break;
                case InteractableType.Weapon:
                    TryReplaceWeapon((WeaponObject)interactable);
                    break;
            }
        }

        private void Accumulate(Accumulation accumulation)
        {
            _accumulations.Pickup(accumulation);
        }

        private void TryReplaceWeapon(WeaponObject weaponObject)
        {
            var replacedWeaponType = _weapons.TryReplaceWeapon(weaponObject.WeaponType);

            if (replacedWeaponType != WeaponType.NoWeapon)
                _weaponFactory.CreateWeaponObject(replacedWeaponType, _hero.Room, _hero.PositionCenter);
        }
    }
}
