using Characters;
using Characters.Common;
using Combat.Attacks;
using Combat.Weapons;
using GameData.Settings;
using UnityEngine;
using static Characters.CharacterAnimator;

namespace Heroes
{
    public class HeroAnimator
    {
        private static readonly int Inverted = Animator.StringToHash("isInverted");

        private float AttackRotation => _weapon.AttackRotation;
        private WeaponAnimator WeaponAnimator => _weapon.Animator;

        private float Rotation => Direction9Utils.Vector2ToRotation(_direction);

        private Vector2 _direction;
        private bool _isInverted;

        private float _lastInversionTime = Mathf.NegativeInfinity;

        private readonly CharacterAnimator _animator;

        private Weapon _weapon;

        public HeroAnimator(CharacterAnimator animator)
        {
            _animator = animator;

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

        private void UpdateAnimator(AnimationStatus status)
        {
            _direction = status.Direction;

            UpdateInversionStatus(Mathf.Abs(Mathf.DeltaAngle(Rotation, AttackRotation)));

            _animator.SetBool(Inverted, _isInverted);
            WeaponAnimator.SetBool(Inverted, _isInverted);
        }

        private void UpdateInversionStatus(float angle)
        {
            if (Mathf.Abs(GameSettings.Hero.InversionAngle - angle) <= GameSettings.Hero.InversionFlickeringDelta)
                UpdateInversionStatusWithLatency(angle);
            else
                SetInversionStatus(angle);
        }

        private void UpdateInversionStatusWithLatency(float angle)
        {
            if (Time.time - _lastInversionTime < GameSettings.Hero.MinTimeToChangeInversion) return;

            SetInversionStatus(angle);
        }

        private void SetInversionStatus(float angle)
        {
            _isInverted = angle > GameSettings.Hero.InversionAngle;
            _lastInversionTime = Time.time;
        }
    }
}
