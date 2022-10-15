using Characters;
using Characters.Common;
using Combat.Attacks;
using Infrastructure.CompositionRoot.Settings;
using UnityEngine;
using Zenject;

namespace Heroes
{
    [RequireComponent(typeof(CharacterAnimator))]
    [RequireComponent(typeof(Animator))]
    public class HeroAnimator : MonoBehaviour
    {
        private static readonly int Inverted = Animator.StringToHash("isInverted");

        [SerializeField] private AttackLocation _attackLocation;
        [SerializeField] private Animator _weaponAnimator;

        private float Rotation => Direction9Utils.Vector2ToRotation(_direction);

        private Vector2 _direction;
        private bool _isInverted;

        private float _lastInversionTime = Mathf.NegativeInfinity;

        private CharacterAnimator _characterAnimator;
        private Animator _animator;

        private HeroSettings _settings;

        [Inject]
        public void Construct(HeroSettings heroSettings)
        {
            _settings = heroSettings;
        }

        public void Initialize()
        {
            FillComponents();

            _characterAnimator.UpdateFinish += UpdateAnimator;
        }

        public void Dispose()
        {
            _characterAnimator.UpdateFinish -= UpdateAnimator;
        }

        private void UpdateAnimator(CharacterAnimator.AnimationStatus status)
        {
            _direction = status.Direction;

            UpdateInversionStatus(Mathf.Abs(Mathf.DeltaAngle(Rotation, _attackLocation.Rotation)));

            _animator.SetBool(Inverted, _isInverted);
            _weaponAnimator.SetBool(Inverted, _isInverted);
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
            if (Time.time - _lastInversionTime < _settings.MinTimeToChangeInversion)
                return;

            SetInversionStatus(angle);
        }

        private void SetInversionStatus(float angle)
        {
            _isInverted = angle > _settings.InversionAngle;
            _lastInversionTime = Time.time;
        }

        private void FillComponents()
        {
            _characterAnimator = GetComponent<CharacterAnimator>();
            _animator = GetComponent<Animator>();
        }
    }
}
