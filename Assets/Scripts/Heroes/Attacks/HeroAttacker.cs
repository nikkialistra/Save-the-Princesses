using System;
using Combat.Attacks;
using Combat.Weapons;
using UnityEngine.InputSystem;

namespace Heroes.Attacks
{
    public class HeroAttacker
    {
        public event Action StrokeStart;

        private Attack Attack => _weapon.Attack;

        private Weapon _weapon;

        private Hero _hero;

        private readonly InputAction _attackAction;

        public HeroAttacker(PlayerInput playerInput)
        {
            _attackAction = playerInput.actions.FindAction("Attack");
        }

        public void Dispose()
        {
            _weapon.Dispose();
        }

        public void SetWeapon(Weapon weapon)
        {
            _weapon = weapon;
        }

        public void Tick()
        {
            _weapon.Tick();

            if (_attackAction.IsPressed())
                TryStroke();
        }

        public void UpdateAttackRotation(float direction)
        {
            Attack.UpdateRotation(direction);
        }

        private void TryStroke()
        {
            if (_weapon.TryStroke())
            {
                Attack.Do(_weapon.LastStroke);
                StrokeStart?.Invoke();
            }
        }
    }
}
