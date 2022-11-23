using System;
using Characters.Common;
using Combat.Attacks.Specs;
using GameData.Weapons;
using UnityEngine;

namespace Combat.Attacks
{
    [RequireComponent(typeof(AttackAnimator))]
    public class Attack : MonoBehaviour
    {
        public event Action Start;
        public event Action End;

        public AttackSpecs Specs { get; private set; }
        public AttackLocation Location { get; private set; }

        public float AttackDistance => _weaponSpecs.AttackDistance;

        public Vector2 Position => transform.position;

        private AttackOrigin _origin;

        private AttackAnimator _animator;

        private WeaponSpecs _weaponSpecs;

        public void Initialize(Vector2 offset, CharacterType characterType)
        {
            _animator = GetComponent<AttackAnimator>();

            InitializeComponents();

            _animator.Start += OnStart;
            _animator.End += OnEnd;

            Location.AlignWithCharacterCenter(offset);
            SetAttackOrigin(characterType);
        }

        public void Dispose()
        {
            _animator.Start -= OnStart;
            _animator.End -= OnEnd;
        }

        public void Tick()
        {
            _animator.Tick();
        }

        public void Do(StrokeType stroke)
        {
            _animator.Stroke(stroke);

            Specs = new AttackSpecs(_origin, _weaponSpecs);
        }

        public void Cancel()
        {
            _animator.CancelStroke();
        }

        public void UpdateForWeaponSpecs(WeaponSpecs weaponSpecs)
        {
            _weaponSpecs = weaponSpecs;
        }

        public void UpdateRotation(float direction)
        {
            if (!_animator.IsStroking)
                Location.UpdateRotation(direction);
        }

        private void SetAttackOrigin(CharacterType characterType)
        {
            _origin = characterType switch
            {
                CharacterType.Hero => AttackOrigin.Hero,
                CharacterType.Enemy => AttackOrigin.Enemy,
                CharacterType.Princess => throw new InvalidOperationException("Princess cannot be attack origin"),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private void InitializeComponents()
        {
            Location = new AttackLocation(transform);
            _animator.Initialize();
        }

        private void OnStart()
        {
            Start?.Invoke();
        }

        private void OnEnd()
        {
            End?.Invoke();
        }
    }
}
