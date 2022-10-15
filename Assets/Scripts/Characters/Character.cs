using System;
using Characters.Common;
using Characters.Traits;
using Combat.Weapons;
using Infrastructure.CompositionRoot.Settings;
using Surrounding.Rooms;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Characters
{
    [RequireComponent(typeof(CharacterHealthHandling))]
    [RequireComponent(typeof(CharacterMoving))]
    [RequireComponent(typeof(CharacterAnimator))]
    [RequireComponent(typeof(CharacterTraits))]
    [RequireComponent(typeof(CharacterBlinking))]
    [RequireComponent(typeof(CharacterMovement))]
    [RequireComponent(typeof(CharacterPathfinding))]
    public class Character : MonoBehaviour
    {
        public event Action Dying;

        public event Action AtStun;
        public event Action AtStunEnd;

        public Room Room { get; private set; }

        public CharacterMoving Moving { get; private set; }
        public CharacterAnimator Animator { get; private set; }
        public CharacterHitsImpact HitsImpact { get; private set; }
        public CharacterSettings Settings { get; private set; }

        public CharacterType Type => _characterType;

        public Vector2 Forward => transform.forward;
        public Vector2 Position => transform.position;
        public Vector2 PositionCenterOffset => new(0, _yPosition);
        public Vector2 PositionCenter => (Vector2)transform.position + new Vector2(0, _yPosition);

        [SerializeField] private CharacterType _characterType;

        [Space]
        [SerializeField] private float _yPosition;

        [Title("Fighting")]
        [SerializeField] private Weapon _weapon;

        private CharacterHealthHandling _healthHandling;
        private CharacterTraits _traits;

        private CharacterBlinking _blinking;
        private CharacterMovement _movement;
        private CharacterPathfinding _pathfinding;

        [Inject]
        public void Construct(CharacterSettings settings)
        {
            Settings = settings;
        }

        public void Initialize()
        {
            FillComponents();
            InitializeComponents();
            InitializeWeapon();

            _healthHandling.Die += OnDie;
        }

        public void Dispose()
        {
            DisposeComponents();
            DisposeWeapon();

            _healthHandling.Die -= OnDie;
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

        private void OnDie()
        {
            Dying?.Invoke();

            if (_characterType != CharacterType.Hero)
                Destroy(gameObject);
        }

        private void FillComponents()
        {
            Moving = GetComponent<CharacterMoving>();
            Animator = GetComponent<CharacterAnimator>();
            HitsImpact = GetComponent<CharacterHitsImpact>();

            _healthHandling = GetComponent<CharacterHealthHandling>();
            _traits = GetComponent<CharacterTraits>();

            _blinking = GetComponent<CharacterBlinking>();
            _movement = GetComponent<CharacterMovement>();
            _pathfinding = GetComponent<CharacterPathfinding>();
        }

        private void InitializeComponents()
        {
            Moving.Initialize();
            Animator.Initialize();
            HitsImpact.Initialize();

            _healthHandling.Initialize();
            _traits.Initialize();

            _blinking.Initialize();
            _movement.Initialize();
            _pathfinding.Initialize();
        }

        private void DisposeComponents()
        {
            Moving.Dispose();

            _healthHandling.Dispose();
            _traits.Dispose();
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
    }
}
