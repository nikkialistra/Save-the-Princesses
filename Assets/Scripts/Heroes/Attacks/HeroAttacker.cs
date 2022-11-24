using System;
using Combat.Attacks;
using Combat.Weapons;
using GameData.Settings;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Heroes.Attacks
{
    public class HeroAttacker
    {
        public event Action StrokeStart;

        private Attack Attack => _weapon.Attack;

        private Weapon _weapon;

        private Hero _hero;

        private readonly HeroWeapons _weapons;

        private readonly InputAction _meleeAttackAction;
        private readonly InputAction _rangedAttackAction;

        public HeroAttacker(HeroWeapons weapons, PlayerInput playerInput)
        {
            _weapons = weapons;

            _meleeAttackAction = playerInput.actions.FindAction("Melee Attack");
            _rangedAttackAction = playerInput.actions.FindAction("Ranged Attack");
        }

        public void Tick()
        {
            _weapons.Tick();

            if (_meleeAttackAction.IsPressed() && _weapons.HasMelee)
                TryMeleeStroke();
            else if (_rangedAttackAction.IsPressed() && _weapons.HasRanged)
                TryRangedStroke();
        }

        public void UpdateAttackRotation(float direction)
        {
            Attack.UpdateRotation(direction);
        }

        private void TryMeleeStroke()
        {
            if (RangedWeaponHitRecently()) return;

            if (_weapons.Melee.TryStroke())
            {
                Attack.Do(_weapon.LastStroke);
                StrokeStart?.Invoke();
            }
        }

        private void TryRangedStroke()
        {
            if (MeleeWeaponHitRecently()) return;

            if (_weapons.Ranged.TryStroke())
            {
                Attack.Do(_weapon.LastStroke);
                StrokeStart?.Invoke();
            }
        }

        private bool RangedWeaponHitRecently()
        {
            if (_weapons.HasRanged == false) return false;

            return Time.time - _weapons.Ranged.AttackEndTime < GameSettings.Hero.TimeAfterLastAttackToChangeWeapon;
        }

        private bool MeleeWeaponHitRecently()
        {
            if (_weapons.HasMelee == false) return false;

            return Time.time - _weapons.Melee.AttackEndTime < GameSettings.Hero.TimeAfterLastAttackToChangeWeapon;
        }
    }
}
