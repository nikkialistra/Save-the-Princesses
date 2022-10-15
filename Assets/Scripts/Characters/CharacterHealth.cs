using System;
using Characters.Stats.Character;
using Infrastructure.CompositionRoot.Settings;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Characters
{
    [RequireComponent(typeof(CharacterStats))]
    public class CharacterHealth : MonoBehaviour
    {
        public event Action Hit;
        public event Action Die;

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
        public float MaxHealth => _stats.MaxHealth.Value;

        private bool IsAlive => Health > 0;

        private float _health;

        private float _invulnerabilityTimeAfterHit;
        private float _lastHitTime;

        private CharacterStats _stats;

        [Inject]
        public void Construct(CharacterSettings settings)
        {
            _invulnerabilityTimeAfterHit = settings.InvulnerabilityTimeAfterHit;
        }

        public void Initialize()
        {
            _stats = GetComponent<CharacterStats>();

            SetInitialValues();

            _stats.MaxHealth.ValueChange += OnMaxHealthChange;
        }

        public void Dispose()
        {
            _stats.MaxHealth.ValueChange -= OnMaxHealthChange;
        }

        [Button]
        public void TakeDamage(int value)
        {
            if (!CheckDamageAllowConditions(value)) return;

            Health -= value;
            InvokeHealthDecreaseEvents();

            _lastHitTime = Time.time;
        }

        [Button]
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
                return false;;

            if (Time.time - _lastHitTime < _invulnerabilityTimeAfterHit)
                return false;

            return IsAlive;
        }

        private void InvokeHealthDecreaseEvents()
        {
            Hit?.Invoke();

            if (Health <= 0)
                Die?.Invoke();
        }
    }
}
