using System;
using Characters;
using Characters.Common;
using Characters.Health;
using Characters.Stats;
using Combat.Weapons;
using Combat.Weapons.Enums;
using Combat.Weapons.Services;
using GameData.Settings;
using GameData.Stats;
using Heroes.Accumulations;
using Heroes.Attacks;
using Rooms;
using Rooms.Services.RepositoryTypes.Princesses;
using Surrounding.Interactables;
using Trains;
using Trains.Characters;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Heroes
{
    [RequireComponent(typeof(Character))]
    [RequireComponent(typeof(TrainCharacter))]
    public class Hero : MonoBehaviour
    {
        public event Action StrokeStart;

        public event Action Slain;

        public bool Active { get; set; }
        public Room Room => _character.Room;

        public CharacterHealth Health => _character.Health;
        public TrainCharacter TrainCharacter { get; private set; }

        public Train Train { get; private set; }

        public HeroAccumulations Accumulations { get; private set; } = new();

        public AllStats Stats => _character.Stats;

        public Vector2 Position => _character.Position;
        public Vector2 PositionCenter => _character.PositionCenter;
        public Vector2 PositionCenterOffset => _character.PositionCenterOffset;

        [SerializeField] private HeroAttackDirection _attackDirection;

        private Character _character;

        private HeroInput _input;
        private HeroMoving _moving;
        private HeroAnimator _animator;
        private HeroWeapons _weapons;
        private HeroAttacker _attacker;
        private HeroInteractor _interactor;

        private HeroTrainStatEffects _trainStatEffects;
        private HeroPrincessGathering _princessGathering;

        private PrincessActiveRepository _activePrincesses;

        private WeaponFactory _weaponFactory;

        private PlayerInput _playerInput;

        [Inject]
        public void Construct(PrincessActiveRepository activePrincesses, WeaponFactory weaponFactory, PlayerInput playerInput)
        {
            _activePrincesses = activePrincesses;
            _weaponFactory = weaponFactory;
            _playerInput = playerInput;
        }

        public void Initialize(Train train, InitialStats initialStats)
        {
            Train = train;

            FillComponents();
            InitializeComponents(initialStats);

            _character.SetCustomHitInvulnerabilityTime(GameSettings.Hero.HitInvulnerabilityTime);

            _weapons.ActiveWeaponChanged += ChangeActiveWeapon;

            _attacker.StrokeStart += OnStrokeStart;
            _character.Slain += OnSlain;
        }

        public void Dispose()
        {
            _weapons.ActiveWeaponChanged -= ChangeActiveWeapon;

            _attacker.StrokeStart += OnStrokeStart;
            _character.Slain -= OnSlain;

            DisposeComponents();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent(typeof(IInteractable)) is IInteractable interactable)
                _interactor.Do(interactable);
        }

        public void SetWeapon(WeaponType weaponType)
        {
            _weapons.TryReplaceWeapon(weaponType);
        }

        public void Tick()
        {
            if (!Active) return;

            _character.Tick();

            _input.Tick();
            _moving.Tick();
            _attackDirection.Tick();
            _attacker.Tick();

            _princessGathering.Tick();
        }

        public void FixedTick()
        {
            if (!Active) return;

            _character.FixedTick();
        }

        public void PlaceAt(Vector3 position)
        {
            transform.position = position;
        }

        public void UpdateAttackRotation(float direction)
        {
            _attacker.UpdateAttackRotation(direction);
        }

        public void ApplyPrincessStatEffects(Trait effects)
        {
            _trainStatEffects.ApplyPrincessStatEffects(effects);
        }

        public void RemovePrincessStatEffects(Trait effects)
        {
            _trainStatEffects.RemovePrincessStatEffects(effects);
        }

        private void ChangeActiveWeapon(Weapon weapon)
        {
            _animator.ChangeWeapon(weapon);
        }

        private void FillComponents()
        {
            _character = GetComponent<Character>();
            TrainCharacter = GetComponent<TrainCharacter>();
        }

        private void InitializeComponents(InitialStats initialStats)
        {
            _character.Initialize(CharacterType.Hero, initialStats);
            TrainCharacter.Initialize(_character, _character.Moving);
            TrainCharacter.SetTrain(Train);

            _attackDirection.Initialize(this);

            _input = new HeroInput(_playerInput);
            _moving = new HeroMoving(_input, _character.Moving);
            _animator = new HeroAnimator(_character.Animator);

            Accumulations = new HeroAccumulations();
            _weapons = new HeroWeapons(_character, _weaponFactory);
            _attacker = new HeroAttacker(_weapons, _playerInput);
            _interactor = new HeroInteractor(this, Accumulations, _weapons, _weaponFactory);

            _trainStatEffects = new HeroTrainStatEffects(_character);
            _princessGathering = new HeroPrincessGathering(_activePrincesses, this, _playerInput);
        }

        private void DisposeComponents()
        {
            _character.Dispose();

            _input.Dispose();

            _animator.Dispose();
            _princessGathering.Dispose();
        }

        private void OnStrokeStart()
        {
            StrokeStart?.Invoke();
        }

        private void OnSlain()
        {
            Active = false;
            Slain?.Invoke();
        }
    }
}
