using System;
using System.Collections;
using Characters.Moving;
using Characters.Moving.HitImpacting;
using Characters.Stats;
using Combat.Attacks;
using GameData.Settings;
using UnityEngine;

namespace Characters.Health
{
    public class CharacterHealth
    {
        public event Action Hit;
        public event Action Slain;

        public event Action HealthChange;
        public event Action MaxHealthChange;

        public float Health
        {
            get => _health;
            private set
            {
                _health = value;
                HealthChange?.Invoke();
            }
        }
        public float MaxHealth => _stats.MaxHealth;

        private bool IsAlive => Health > 0;

        private float _health;

        private float _invulnerabilityTimeAfterHit;
        private float _lastHitTime;

        private readonly CharacterAttackHandling _attackHandling;

        private readonly Character _character;
        private readonly CharacterMoving _moving;

        private readonly AllStats _stats;

        private Coroutine _takingDamageCoroutine;

        public CharacterHealth(Character character, CharacterMoving moving, AllStats stats)
        {
            _character = character;
            _moving = moving;
            _stats = stats;

            _attackHandling = new CharacterAttackHandling(_character, this);

            Initialize();

            _attackHandling.HitImpacted += OnHitImpacted;
            _stats.MaxHealthStat.ValueChange += OnMaxHealthChange;
        }

        public void Dispose()
        {
            _attackHandling.HitImpacted -= OnHitImpacted;
            _stats.MaxHealthStat.ValueChange -= OnMaxHealthChange;
        }

        public void TakeAttack(Attack attack)
        {
            _attackHandling.TakeAttack(attack);
        }

        public void TakeDamage(int value)
        {
            if (!CheckDamageAllowConditions(value)) return;

            Health -= value;
            InvokeHealthDecreaseEvents();

            _lastHitTime = Time.time;
        }

        public void TakeDamageContinuously(int value, float interval)
        {
            _takingDamageCoroutine = _character.StartCoroutine(CTakingDamage(value, interval));
        }

        public void StopTakingDamage()
        {
            if (_takingDamageCoroutine != null)
            {
                _character.StopCoroutine(_takingDamageCoroutine);
                _takingDamageCoroutine = null;
            }
        }

        public void TakeHealing(int value)
        {
            if (value <= 0)
                throw new ArgumentException("Healing cannot be less than or equal to zero");

            Health = Health + value <= MaxHealth
                ? Health + value
                : MaxHealth;
        }

        public void SetCustomHitInvulnerabilityTime(float value)
        {
            _invulnerabilityTimeAfterHit = value;
        }

        private void Initialize()
        {
            _invulnerabilityTimeAfterHit = GameSettings.Character.InvulnerabilityTimeAfterHit;

            Health = MaxHealth;
        }

        private IEnumerator CTakingDamage(int value, float interval)
        {
            while (true)
            {
                TakeDamage(value);

                yield return new WaitForSeconds(interval);
            }
        }

        private void OnMaxHealthChange(float value)
        {
            CutHealth(value);
            MaxHealthChange?.Invoke();
        }

        private void CutHealth(float value)
        {
            if (Health > value)
                Health = value;
        }

        private bool CheckDamageAllowConditions(int value)
        {
            if (value <= 0)
                return false;

            if (Time.time - _lastHitTime < _invulnerabilityTimeAfterHit)
                return false;

            return IsAlive;
        }

        private void InvokeHealthDecreaseEvents()
        {
            Hit?.Invoke();

            if (Health <= 0)
                Slain?.Invoke();
        }

        private void OnHitImpacted(TakenHitImpact hitImpact)
        {
            _moving.TakeHitImpact(hitImpact);
        }
    }
}
