using System;
using System.Collections.Generic;
using System.Threading;
using Combat;
using Combat.Attacks;
using Combat.Attacks.Specs;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Characters
{
    [RequireComponent(typeof(Character))]
    [RequireComponent(typeof(CharacterHealth))]
    [RequireComponent(typeof(CharacterHitsImpact))]
    public class CharacterHealthHandling : MonoBehaviour, IDamageable
    {
        public event Action Hit;
        public event Action Slay;

        private readonly HashSet<AttackSpecs> _damagedAttacks = new();

        private readonly CancellationTokenSource _takingDamageToken = new();

        private Character _character;
        private CharacterHealth _health;
        private CharacterHitsImpact _hitsImpact;

        public void Initialize()
        {
            FillComponents();

            _health.Hit += OnHit;
            _health.Slay += OnSlay;
        }

        public void Dispose()
        {
            _takingDamageToken.Dispose();

            _health.Hit += OnHit;
            _health.Slay -= OnSlay;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            HandleIfAttack(other);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            HandleIfAttack(other);
        }

        private void HandleIfAttack(Collider2D other)
        {
            var attack = other.GetComponentInParent<Attack>();

            if (attack == null) return;

            Handle(attack, attack.Specs);
        }

        private void Handle(Attack attack, AttackSpecs attackSpecs)
        {
            if (attackSpecs.Origin.IsFriendlyFor(_character.Type)) return;

            if (!attackSpecs.IsPenetrable && IsTakenOnce(attackSpecs)) return;

            var attackDirection = (_character.PositionCenter - attack.Position).normalized;

            TakeAttackDamage(attackSpecs.Damage);
            _hitsImpact.Take(attackDirection, attackSpecs.Knockback, attackSpecs.Stun);
        }

        private void OnHit()
        {
            Hit?.Invoke();
        }

        private void OnSlay()
        {
            Slay?.Invoke();
        }

        public void TakeDamage(int value)
        {
            _health.TakeDamage(value);
        }

        public void TakeDamageContinuously(int value, float interval)
        {
            _takingDamageToken.Cancel();

            TakingDamage(value, interval).AttachExternalCancellation(_takingDamageToken.Token);
        }

        public void StopTakingDamage()
        {
            _takingDamageToken.Cancel();
        }

        private async UniTask TakingDamage(int value, float interval)
        {
            while (true)
            {
                TakeDamage(value);
                await UniTask.Delay(TimeSpan.FromSeconds(interval));
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

        private void FillComponents()
        {
            _character = GetComponent<Character>();
            _health = GetComponent<CharacterHealth>();
            _hitsImpact = GetComponent<CharacterHitsImpact>();
        }
    }
}
