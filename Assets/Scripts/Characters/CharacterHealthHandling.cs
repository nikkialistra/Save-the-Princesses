using System;
using System.Collections;
using System.Collections.Generic;
using Combat;
using Combat.Attacks;
using Combat.Attacks.Specs;
using UnityEngine;

namespace Characters
{
    public class CharacterHealthHandling : IDamageable
    {
        public event Action Hit;
        public event Action Slay;

        private readonly HashSet<AttackSpecs> _damagedAttacks = new();

        private readonly Character _character;
        private readonly CharacterHealth _health;
        private readonly CharacterHitsImpact _hitsImpact;

        private Coroutine _takingDamageCoroutine;

        public CharacterHealthHandling(Character character, CharacterHealth health, CharacterHitsImpact hitsImpact)
        {
            _character = character;
            _health = health;
            _hitsImpact = hitsImpact;

            _health.Hit += OnHit;
            _health.Slay += OnSlay;
        }

        public void Dispose()
        {
            _health.Hit += OnHit;
            _health.Slay -= OnSlay;
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
            _hitsImpact.Take(attackDirection, attackSpecs.Knockback, attackSpecs.Stun);
        }

        public void TakeDamage(int value)
        {
            _health.TakeDamage(value);
        }

        public void TakeDamageContinuously(int value, float interval)
        {
            _takingDamageCoroutine = _character.StartCoroutine(CTakingDamage(value, interval));
        }

        private IEnumerator CTakingDamage(int value, float interval)
        {
            while (true)
            {
                TakeDamage(value);

                yield return new WaitForSeconds(interval);
            }
        }

        public void StopTakingDamage()
        {
            if (_takingDamageCoroutine != null)
            {
                _character.StopCoroutine(_takingDamageCoroutine);
                _takingDamageCoroutine = null;
            }
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

        private void OnHit()
        {
            Hit?.Invoke();
        }

        private void OnSlay()
        {
            Slay?.Invoke();
        }
    }
}
