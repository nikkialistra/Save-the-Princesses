using System;
using Combat.Attacks;
using Combat.Weapons;
using Combat.Weapons.Concrete;
using UnityEngine.InputSystem;

namespace Heroes.Attacks
{
    public class HeroAttacker
    {
        public event Action StrokeStart;

        private Attack Attack => _concreteWeapon.Attack;

        private ConcreteWeapon _concreteWeapon;

        private readonly InputAction _attackAction;

        public HeroAttacker(PlayerInput playerInput)
        {
            _attackAction = playerInput.actions.FindAction("Attack");
        }

        public void SetConcreteWeapon(ConcreteWeapon concreteWeapon)
        {
            _concreteWeapon = concreteWeapon;
        }

        public void Tick()
        {
            if (_attackAction.IsPressed())
                TryStroke();
        }

        public void UpdateAttackRotation(float direction)
        {
            Attack.UpdateRotation(direction);
        }

        private void TryStroke()
        {
            if (_concreteWeapon.TryStroke())
            {
                Attack.Do(_concreteWeapon.LastStroke);
                StrokeStart?.Invoke();
            }
        }
    }
}
