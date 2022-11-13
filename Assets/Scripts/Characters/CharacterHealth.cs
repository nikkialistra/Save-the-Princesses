using System;
using Characters.Stats;
using Infrastructure.Installers.Game.Settings;
using UnityEngine;

namespace Characters
{
    public class CharacterHealth
    {
        public event Action Hit;
        public event Action Slay;

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

        private readonly AllStats _stats;

        public CharacterHealth(AllStats stats, CharacterSettings settings)
        {
            _stats = stats;
            _invulnerabilityTimeAfterHit = settings.InvulnerabilityTimeAfterHit;

            SetInitialValues();

            _stats.MaxHealthStat.ValueChange += OnMaxHealthChange;
        }

        public void Dispose()
        {
            _stats.MaxHealthStat.ValueChange -= OnMaxHealthChange;
        }

        public void TakeDamage(int value)
        {
            if (!CheckDamageAllowConditions(value)) return;

            Health -= value;
            InvokeHealthDecreaseEvents();

            _lastHitTime = Time.time;
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

        private void SetInitialValues()
        {
            Health = MaxHealth;
        }

        private void OnMaxHealthChange(float value)
        {
            MaxHealthChange?.Invoke();
            CutHealth(value);
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
                Slay?.Invoke();
        }
    }
}
