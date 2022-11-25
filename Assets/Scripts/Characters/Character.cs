using System;
using Characters.Common;
using Characters.Health;
using Characters.Moving;
using Characters.Stats;
using Combat.Attacks;
using GameData.Settings;
using GameData.Stats;
using Rooms;
using UnityEngine;

namespace Characters
{
    [RequireComponent(typeof(AllStats))]
    [RequireComponent(typeof(CharacterMoving))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Character : MonoBehaviour
    {
        public event Action Hit;
        public event Action Slain;

        public event Action<bool> StunChange;
        public event Action<bool> BlinkChange;

        public Room Room { get; private set; }

        public AllStats Stats { get; private set; }

        public CharacterHealth Health { get; private set; }
        public CharacterMoving Moving { get; private set; }
        public CharacterAnimator Animator { get; private set; }

        public CharacterType Type { get; private set; }

        public Vector2 Forward => transform.forward;
        public Vector2 Position => transform.position;
        public Vector2 PositionCenterOffset => new(0, _yPosition);
        public Vector2 PositionCenter => (Vector2)transform.position + new Vector2(0, _yPosition);
        public float DirectionChangeTime { get; set; }

        public bool HasImpact => Moving.HasImpact;

        [SerializeField] private float _yPosition;

        private bool _active;

        private CharacterBlinking _blinking;

        private Animator _animator;
        private SpriteRenderer _spriteRenderer;

        public void Initialize(CharacterType characterType, InitialStats initialStats)
        {
            Type = characterType;

            DirectionChangeTime = GameSettings.Character.DirectionChangeTime;

            FillComponents();
            InitializeComponents(initialStats);

            Health.Hit += OnHit;
            Health.Slain += OnSlain;

            _blinking.BlinkChange += OnBlinkChange;
        }

        public void Dispose()
        {
            DisposeComponents();

            Health.Hit -= OnHit;
            Health.Slain -= OnSlain;

            _blinking.BlinkChange -= OnBlinkChange;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            TakeIfAttack(other);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            TakeIfAttack(other);
        }

        public void Tick()
        {
            Moving.Tick();
            Animator.Tick();
        }

        public void FixedTick()
        {
            Moving.FixedTick();
        }

        public void PlaceInRoom(Room room)
        {
            Room = room;
        }

        public void Stun(bool value)
        {
            StunChange?.Invoke(value);
        }

        public void AddTrait(Trait trait)
        {
            Stats.AddTrait(trait);
        }

        public void RemoveTrait(Trait trait)
        {
            Stats.RemoveTrait(trait);
        }

        public void SetCustomHitInvulnerabilityTime(float value)
        {
            Health.SetCustomHitInvulnerabilityTime(value);
        }

        public void TakeDamage(int value)
        {
            Health.TakeDamage(value);
        }

        public void TakeDamageContinuously(int value, float interval)
        {
            Health.TakeDamageContinuously(value, interval);
        }

        public void StopTakingDamage()
        {
            Health.StopTakingDamage();
        }

        private void TakeIfAttack(Collider2D other)
        {
            var attack = other.GetComponentInParent<Attack>();

            if (attack != null)
                Health.TakeAttack(attack);
        }

        private void FillComponents()
        {
            Stats = GetComponent<AllStats>();
            Moving = GetComponent<CharacterMoving>();

            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void InitializeComponents(InitialStats initialStats)
        {
            Stats.Initialize(initialStats);

            Moving.Initialize(this, Stats);
            Animator = new CharacterAnimator(this, _animator, Moving, Type);

            Health = new CharacterHealth(this, Moving, Stats);

            _blinking = new CharacterBlinking(this, _spriteRenderer);
        }

        private void DisposeComponents()
        {
            Health.Dispose();
            Moving.Dispose();

            _blinking.Dispose();
        }

        private void OnHit()
        {
            Hit?.Invoke();
        }

        private void OnSlain()
        {
            Slain?.Invoke();

            if (Type != CharacterType.Hero)
            {
                Dispose();
                Destroy(gameObject);
            }
        }

        private void OnBlinkChange(bool status)
        {
            BlinkChange?.Invoke(status);
        }
    }
}
