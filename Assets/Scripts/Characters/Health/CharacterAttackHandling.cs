using System;
using System.Collections.Generic;
using Characters.Moving;
using Characters.Moving.HitImpacting;
using Combat.Attacks;
using Combat.Attacks.Specs;
using UnityEngine;

namespace Characters.Health
{
    public class CharacterAttackHandling
    {
        public event Action<TakenHitImpact> HitImpacted;

        private readonly HashSet<AttackSpecs> _damagedAttacks = new();

        private readonly Character _character;
        private readonly CharacterHealth _health;

        public CharacterAttackHandling(Character character, CharacterHealth health)
        {
            _character = character;
            _health = health;
        }

        public void TakeAttack(Attack attack)
        {
            var attackSpecs = attack.Specs;

            if (attackSpecs.Origin.IsFriendlyFor(_character.Type)) return;

            if (!attackSpecs.IsPenetrable && IsTakenOnce(attackSpecs)) return;

            ConsumeAttack(attack, attackSpecs);
        }

        private void ConsumeAttack(Attack attack, AttackSpecs attackSpecs)
        {
            var attackDirection = (_character.PositionCenter - attack.Position).normalized;

            TakeAttackDamage(attackSpecs.Damage);
            NotifyAboutImpact(attackSpecs, attackDirection);
        }

        private void NotifyAboutImpact(AttackSpecs attackSpecs, Vector2 attackDirection)
        {
            var attackImpact = new TakenHitImpact
            {
                Direction = attackDirection,
                Knockback = attackSpecs.Knockback,
                Stun = attackSpecs.Stun
            };

            HitImpacted?.Invoke(attackImpact);
        }

        private bool IsTakenOnce(AttackSpecs attackSpecs)
        {
            if (_damagedAttacks.Contains(attackSpecs))
                return true;

            _damagedAttacks.Add(attackSpecs);
            return false;
        }

        private void TakeAttackDamage(int value)
        {
            if (value <= 0) throw new ArgumentException("Damage must be more than zero");

            _health.TakeDamage(value);
        }
    }
}
