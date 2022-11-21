using Characters;
using Combat.Weapons;
using Heroes;
using UnityEngine;

namespace Enemies
{
    public class EnemyAttacker
    {
        public float AttackDistance => _weapon.Attack.AttackDistance;

        private Vector2 DirectionToHero => _hero.PositionCenter - (_character.Position + _weapon.Attack.CenterOffset);
        private float AngleToHero => Mathf.Atan2(DirectionToHero.y, DirectionToHero.x) * Mathf.Rad2Deg;

        private Weapon _weapon;

        private readonly Character _character;

        private readonly Hero _hero;

        public EnemyAttacker(Character character, Hero hero)
        {
            _character = character;
            _hero = hero;

            _character.StunChange += OnStunChange;
        }

        public void Dispose()
        {
            _weapon.Dispose();

            _character.StunChange -= OnStunChange;
        }

        public void Tick()
        {
            _weapon.Tick();
        }

        public void SetWeapon(Weapon weapon)
        {
            _weapon = weapon;
        }

        public void UpdateAttackRotation()
        {
            _weapon.Attack.UpdateRotation(AngleToHero);
        }

        public void TryAttack()
        {
            if (_weapon.TryStroke())
                _weapon.Attack.Do(_weapon.LastStroke);
        }

        private void CancelAttack()
        {
            _weapon.ResetStroke();
            _weapon.Attack.Cancel();
        }

        private void OnStunChange(bool stunned)
        {
            if (stunned)
                CancelAttack();
        }
    }
}
