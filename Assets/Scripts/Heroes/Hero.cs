using System;
using Characters;
using Characters.Stats;
using Characters.Traits;
using Combat.Weapons;
using GameData.Stats;
using Heroes.Attacks;
using Infrastructure.Installers.Game.Settings;
using Princesses.Services.Repositories;
using Trains.Characters;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Heroes
{
    [RequireComponent(typeof(TrainCharacter))]
    [RequireComponent(typeof(Character))]
    public class Hero : MonoBehaviour, ITickable
    {
        public event Action StrokeStart;

        public event Action Slain;

        public CharacterHealth Health => _character.Health;
        public TrainCharacter TrainCharacter { get; private set; }

        public AllStats Stats => _character.Stats;

        public Vector2 Position => _character.Position;
        public Vector2 PositionCenter => _character.PositionCenter;
        public Vector2 PositionCenterOffset => _character.PositionCenterOffset;

        private bool _active;

        private HeroInput _input;
        private HeroMoving _moving;
        private HeroAnimator _animator;
        private HeroAttacker _attacker;

        private HeroTrainStatEffects _trainStatEffects;
        private HeroPrincessGathering _princessGathering;

        private Character _character;

        private InitialStats _initialStats;

        private Weapon _weapon;

        private PrincessActiveRepository _activePrincesses;

        private HeroSettings _settings;

        private PlayerInput _playerInput;

        [Inject]
        public void Construct(InitialStats initialStats, PrincessActiveRepository activePrincesses,
            HeroSettings settings, PlayerInput playerInput)
        {
            _initialStats = initialStats;
            _activePrincesses = activePrincesses;
            _settings = settings;
            _playerInput = playerInput;
        }

        public void Initialize()
        {
            FillComponents();
            InitializeComponents();

            TrainCharacter.SetAsHero();
            _character.SetCustomHitInvulnerabilityTime(_settings.HitInvulnerabilityTime);

            _attacker.StrokeStart += OnStrokeStart;
            _character.Slain += OnSlain;
        }

        public void SetWeapon(Weapon weapon)
        {
            _weapon = weapon;
        }

        public void Dispose()
        {
            _attacker.StrokeStart += OnStrokeStart;
            _character.Slain -= OnSlain;

            DisposeComponents();
        }

        public void Activate()
        {
            _active = true;
        }

        public void Deactivate()
        {
            _active = false;
        }

        public void Tick()
        {
            if (!_active) return;

            _input.Tick();
            _moving.Tick();
            _attacker.Tick();

            _princessGathering.Tick();
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
            Slain?.Invoke();
        }

        private void FillComponents()
        {
            _character = GetComponent<Character>();

            _input = new HeroInput(_playerInput, _settings);
            _moving = new HeroMoving(_input, _character.Moving);
            _animator = new HeroAnimator(_character.Animator, _settings);
            _attacker = new HeroAttacker(_playerInput);

            TrainCharacter = GetComponent<TrainCharacter>();

            _trainStatEffects = new HeroTrainStatEffects(_character);
            _princessGathering = new HeroPrincessGathering(_activePrincesses, transform, _playerInput, _settings);
        }

        private void InitializeComponents()
        {
            _character.Initialize(_initialStats);
            TrainCharacter.Initialize();
        }

        private void DisposeComponents()
        {
            _character.Dispose();

            _animator.Dispose();
            _princessGathering.Dispose();
        }
    }
}
