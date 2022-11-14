using System;
using Characters.Common;
using Characters.Moving;
using Characters.Stats;
using Combat.Attacks;
using Combat.Weapons;
using GameData.Stats;
using Surrounding.Rooms;
using UnityEngine;
using Zenject;

namespace Characters
{
    [RequireComponent(typeof(CharacterMoving))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Character : MonoBehaviour, ITickable, IFixedTickable
    {
        public event Action Hit;
        public event Action Slain;

        public event Action AtStun;
        public event Action AtStunEnd;

        public bool Active { get; set; }
        public Room Room { get; private set; }

        public AllStats Stats { get; private set; }

        public CharacterHealth Health { get; private set; }
        public CharacterMoving Moving { get; private set; }
        public CharacterAnimator Animator { get; private set; }
        public CharacterHitsImpact HitsImpact { get; private set; }

        public CharacterType Type { get; private set; }

        public Vector2 Forward => transform.forward;
        public Vector2 Position => transform.position;
        public Vector2 PositionCenterOffset => new(0, _yPosition);
        public Vector2 PositionCenter => (Vector2)transform.position + new Vector2(0, _yPosition);

        [SerializeField] private float _yPosition;

        private Weapon _weapon;

        private bool _active;

        private CharacterHealthHandling _healthHandling;

        private CharacterBlinking _blinking;

        private Animator _animator;
        private SpriteRenderer _spriteRenderer;

        public void Initialize(CharacterType characterType, InitialStats initialStats)
        {
            Type = characterType;

            FillComponents();
            InitializeComponents(initialStats);

            _healthHandling.Hit += OnHit;
            _healthHandling.Slay += OnSlay;
        }

        public void Dispose()
        {
            DisposeComponents();

            _healthHandling.Hit -= OnHit;
            _healthHandling.Slay -= OnSlay;
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
            if (!Active) return;

            Moving.Tick();
            Animator.Tick();
        }

        public void FixedTick()
        {
            if (!Active) return;

            Moving.FixedTick();
        }

        public void PlaceInRoom(Room room)
        {
            Room = room;
        }

        public void SetWeapon(Weapon weapon)
        {
            _weapon = weapon;
            _weapon.Initialize(this, _blinking);
        }

        public void Stun(bool value)
        {
            if (value == true)
                AtStun?.Invoke();
            else
                AtStunEnd?.Invoke();
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

        private void TakeIfAttack(Collider2D other)
        {
            var attack = other.GetComponentInParent<Attack>();

            if (attack != null)
                _healthHandling.TakeAttack(attack);
        }

        private void FillComponents()
        {
            Moving = GetComponent<CharacterMoving>();

            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void InitializeComponents(InitialStats initialStats)
        {
            Stats = new AllStats(initialStats);

            Moving.Initialize(this);
            Animator = new CharacterAnimator(_animator, Moving, Type);

            Health = new CharacterHealth(Stats);
            HitsImpact = new CharacterHitsImpact(Stats);
            _healthHandling = new CharacterHealthHandling(this, Health, HitsImpact);

            _blinking = new CharacterBlinking(this, _spriteRenderer);
        }

        private void DisposeComponents()
        {
            Health.Dispose();
            Moving.Dispose();

            _healthHandling.Dispose();

            _blinking.Dispose();

            if (_weapon != null)
                _weapon.Dispose();
        }

        private void OnHit()
        {
            Hit?.Invoke();
        }

        private void OnSlay()
        {
            Slain?.Invoke();

            if (Type != CharacterType.Hero)
            {
                Dispose();
                Destroy(gameObject);
            }
        }
    }
}
