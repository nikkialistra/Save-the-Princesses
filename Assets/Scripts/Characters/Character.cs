using System;
using Characters.Common;
using Characters.Moving;
using Characters.Stats;
using Characters.Traits;
using Combat.Attacks;
using Combat.Weapons;
using GameData.Stats;
using Infrastructure.Installers.Game.Settings;
using Sirenix.OdinInspector;
using Surrounding.Rooms;
using UnityEngine;
using Zenject;

namespace Characters
{
    [RequireComponent(typeof(CharacterAnimator))]
    [RequireComponent(typeof(CharacterTraits))]
    [RequireComponent(typeof(CharacterMoving))]
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
        public CharacterSettings Settings { get; private set; }

        public CharacterType Type => _characterType;

        public Vector2 Forward => transform.forward;
        public Vector2 Position => transform.position;
        public Vector2 PositionCenterOffset => new(0, _yPosition);
        public Vector2 PositionCenter => (Vector2)transform.position + new Vector2(0, _yPosition);

        public bool HasImpact => HitsImpact.HasImpact;

        [SerializeField] private CharacterType _characterType;

        [Space]
        [SerializeField] private float _yPosition;

        [Title("Fighting")]
        [SerializeField] private Weapon _weapon;

        private bool _active;

        private CharacterHealthHandling _healthHandling;
        private CharacterTraits _traits;

        private CharacterBlinking _blinking;

        private SpriteRenderer _spriteRenderer;

        [Inject]
        public void Construct(CharacterSettings settings)
        {
            Settings = settings;
        }

        public void Initialize(InitialStats initialStats)
        {
            FillComponents();
            InitializeComponents(initialStats);
            InitializeWeapon();

            _healthHandling.Hit += OnHit;
            _healthHandling.Slay += OnSlay;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            TakeIfAttack(other);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            TakeIfAttack(other);
        }

        public void Dispose()
        {
            DisposeComponents();
            DisposeWeapon();

            _healthHandling.Hit -= OnHit;
            _healthHandling.Slay -= OnSlay;
        }

        public void Tick()
        {
            if (!Active) return;

            Moving.Tick();
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

        public void Stun(bool value)
        {
            if (value == true)
                AtStun?.Invoke();
            else
                AtStunEnd?.Invoke();
        }

        public void AddTrait(Trait trait)
        {
            _traits.Add(trait);
        }

        public void RemoveTrait(Trait trait)
        {
            _traits.Remove(trait);
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
            _spriteRenderer = GetComponent<SpriteRenderer>();

            Animator = GetComponent<CharacterAnimator>();

            _traits = GetComponent<CharacterTraits>();
        }

        private void InitializeComponents(InitialStats initialStats)
        {
            Stats = new AllStats(initialStats);

            Moving.Initialize(this, Settings);

            Animator.Initialize();

            Health = new CharacterHealth(Stats, Settings);
            HitsImpact = new CharacterHitsImpact(Stats);
            _healthHandling = new CharacterHealthHandling(this, Health, HitsImpact);

            _traits.Initialize();

            _blinking = new CharacterBlinking(this, _spriteRenderer, Settings);
        }

        private void DisposeComponents()
        {
            Health.Dispose();
            Moving.Dispose();

            _healthHandling.Dispose();
            _traits.Dispose();

            _blinking.Dispose();
        }

        private void InitializeWeapon()
        {
            if (_weapon != null)
                _weapon.Initialize(this, _blinking);
        }

        private void DisposeWeapon()
        {
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

            if (_characterType != CharacterType.Hero)
                Destroy(gameObject);
        }
    }
}
