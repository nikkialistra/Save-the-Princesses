using System;
using Characters;
using Characters.Common;
using Characters.Health;
using Characters.Stats;
using Combat.Weapons;
using GameData.Settings;
using GameData.Stats;
using Heroes.Accumulations;
using Heroes.Attacks;
using Princesses.Services.Repositories;
using Surrounding.Collectables;
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

        public CharacterHealth Health => Character.Health;
        public Character Character { get; private set; }
        public TrainCharacter TrainCharacter { get; private set; }

        public HeroCollector Collector { get; private set; } = new();

        public AllStats Stats => Character.Stats;

        public Vector2 Position => Character.Position;
        public Vector2 PositionCenter => Character.PositionCenter;
        public Vector2 PositionCenterOffset => Character.PositionCenterOffset;

        [SerializeField] private HeroAttackDirection _attackDirection;

        private HeroInput _input;
        private HeroMoving _moving;
        private HeroAnimator _animator;
        private HeroAttacker _attacker;

        private HeroTrainStatEffects _trainStatEffects;
        private HeroPrincessGathering _princessGathering;

        private PrincessActiveRepository _activePrincesses;

        private Train _train;

        private PlayerInput _playerInput;

        [Inject]
        public void Construct(PrincessActiveRepository activePrincesses, Train train, PlayerInput playerInput)
        {
            _activePrincesses = activePrincesses;
            _train = train;
            _playerInput = playerInput;
        }

        public void Initialize(InitialStats initialStats)
        {
            FillComponents();
            InitializeComponents(initialStats);

            Character.SetCustomHitInvulnerabilityTime(GameSettings.Hero.HitInvulnerabilityTime);

            _attacker.StrokeStart += OnStrokeStart;
            Character.Slain += OnSlain;
        }

        public void Dispose()
        {
            _attacker.StrokeStart += OnStrokeStart;
            Character.Slain -= OnSlain;

            DisposeComponents();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent(typeof(Collectable)) is Collectable collectable)
                Collector.Pickup(collectable);
        }

        public void SetWeapon(Weapon weapon)
        {
            _attacker.SetWeapon(weapon);
            _animator.SetWeapon(weapon);
        }

        public void Tick()
        {
            if (!Active) return;

            Character.Tick();

            _input.Tick();
            _moving.Tick();
            _attackDirection.Tick();
            _attacker.Tick();

            _princessGathering.Tick();
        }

        public void FixedTick()
        {
            if (!Active) return;

            Character.FixedTick();
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

        private void OnStrokeStart()
        {
            StrokeStart?.Invoke();
        }

        private void OnSlain()
        {
            Active = false;
            Slain?.Invoke();
        }

        private void FillComponents()
        {
            Character = GetComponent<Character>();
            TrainCharacter = GetComponent<TrainCharacter>();
        }

        private void InitializeComponents(InitialStats initialStats)
        {
            Character.Initialize(CharacterType.Hero, initialStats);
            TrainCharacter.Initialize(Character, Character.Moving, _train);

            _attackDirection.Initialize(this);

            _input = new HeroInput(_playerInput);
            _moving = new HeroMoving(_input, Character.Moving);
            _animator = new HeroAnimator(Character.Animator);
            _attacker = new HeroAttacker(_playerInput);

            Collector = new HeroCollector();

            _trainStatEffects = new HeroTrainStatEffects(Character);
            _princessGathering = new HeroPrincessGathering(_activePrincesses, transform, _playerInput);
        }

        private void DisposeComponents()
        {
            Character.Dispose();
            _attacker.Dispose();

            _animator.Dispose();
            _princessGathering.Dispose();
        }
    }
}
