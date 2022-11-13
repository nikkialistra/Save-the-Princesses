using Characters;
using Characters.Common;
using Combat.Attacks;
using Combat.Weapons;
using Infrastructure.Installers.Game.Settings;
using UnityEngine;

namespace Heroes
{
    public class HeroAnimator
    {
        private static readonly int Inverted = Animator.StringToHash("isInverted");

        private AttackLocation AttackLocation => _weapon.AttackLocation;
        private WeaponAnimator WeaponAnimator => _weapon.Animator;

        private float Rotation => Direction9Utils.Vector2ToRotation(_direction);

        private Vector2 _direction;
        private bool _isInverted;

        private float _lastInversionTime = Mathf.NegativeInfinity;

        private readonly CharacterAnimator _animator;

        private Weapon _weapon;

        private readonly HeroSettings _settings;

        public HeroAnimator(CharacterAnimator animator, HeroSettings heroSettings)
        {
            _animator = animator;
            _settings = heroSettings;

            _animator.UpdateFinish += UpdateAnimator;
        }

        public void SetWeapon(Weapon weapon)
        {
            _weapon = weapon;
        }

        public void Dispose()
        {
            _animator.UpdateFinish -= UpdateAnimator;
        }

        private void UpdateAnimator(CharacterAnimator.AnimationStatus status)
        {
            _direction = status.Direction;

            UpdateInversionStatus(Mathf.Abs(Mathf.DeltaAngle(Rotation, AttackLocation.Rotation)));

            _animator.SetBool(Inverted, _isInverted);
            WeaponAnimator.SetBool(Inverted, _isInverted);
        }

        private void UpdateInversionStatus(float angle)
        {
            if (Mathf.Abs(_settings.InversionAngle - angle) <= _settings.InversionFlickeringDelta)
                UpdateInversionStatusWithLatency(angle);
            else
                SetInversionStatus(angle);
        }

        private void UpdateInversionStatusWithLatency(float angle)
        {
            if (Time.time - _lastInversionTime < _settings.MinTimeToChangeInversion) return;

            SetInversionStatus(angle);
        }

        private void SetInversionStatus(float angle)
        {
            _isInverted = angle > _settings.InversionAngle;
            _lastInversionTime = Time.time;
        }
    }
}
