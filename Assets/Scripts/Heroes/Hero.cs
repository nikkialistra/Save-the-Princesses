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
    public class Hero : MonoBehaviour, ITickable, IFixedTickable
    {
        public event Action StrokeStart;

        public event Action Slain;

        public bool Active { get; set; }

        public CharacterHealth Health => _character.Health;
        public TrainCharacter TrainCharacter { get; private set; }

        public HeroCollector Collector { get; private set; } = new();

        public AllStats Stats => _character.Stats;

        public Vector2 Position => _character.Position;
        public Vector2 PositionCenter => _character.PositionCenter;
        public Vector2 PositionCenterOffset => _character.PositionCenterOffset;

        [SerializeField] private HeroAttackDirection _attackDirection;

        private HeroInput _input;
        private HeroMoving _moving;
        private HeroAnimator _animator;
        private HeroAttacker _attacker;

        private HeroTrainStatEffects _trainStatEffects;
        private HeroPrincessGathering _princessGathering;

        private Character _character;

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

            _character.SetCustomHitInvulnerabilityTime(GameSettings.Hero.HitInvulnerabilityTime);

            _attacker.StrokeStart += OnStrokeStart;
            _character.Slain += OnSlain;
        }

        public void Dispose()
        {
            _attacker.StrokeStart += OnStrokeStart;
            _character.Slain -= OnSlain;

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
            _character = GetComponent<Character>();
            TrainCharacter = GetComponent<TrainCharacter>();
        }

        private void InitializeComponents(InitialStats initialStats)
        {
            _character.Initialize(CharacterType.Hero, initialStats);
            TrainCharacter.Initialize(_character, _character.Moving, _train);

            _input = new HeroInput(_playerInput);
            _moving = new HeroMoving(_input, _character.Moving);
            _animator = new HeroAnimator(_character.Animator);
            _attacker = new HeroAttacker(_playerInput);

            Collector = new HeroCollector();

            _trainStatEffects = new HeroTrainStatEffects(_character);
            _princessGathering = new HeroPrincessGathering(_activePrincesses, transform, _playerInput);
        }

        private void DisposeComponents()
        {
            _character.Dispose();
            _attacker.Dispose();

            _animator.Dispose();
            _princessGathering.Dispose();
        }
    }
}
