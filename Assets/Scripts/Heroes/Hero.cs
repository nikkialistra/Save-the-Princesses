using System;
using Characters;
using Characters.Stats;
using Characters.Traits;
using Combat.Attacks;
using GameData.Heroes;
using GameData.Stats;
using Heroes.Attacks;
using Infrastructure.Installers.Game.Settings;
using Trains.Characters;
using UnityEngine;
using Zenject;

namespace Heroes
{
    [RequireComponent(typeof(HeroTrainStatEffects))]
    [RequireComponent(typeof(HeroAttacker))]
    [RequireComponent(typeof(HeroAnimator))]
    [RequireComponent(typeof(HeroInput))]
    [RequireComponent(typeof(HeroMoving))]
    [RequireComponent(typeof(HeroPrincessGathering))]
    [RequireComponent(typeof(TrainCharacter))]
    [RequireComponent(typeof(CharacterHealth))]
    [RequireComponent(typeof(Character))]
    public class Hero : MonoBehaviour, ITickable
    {
        public event Action Slain;

        public Attack Attack => _attack;

        public CharacterHealth Health => _character.Health;
        public HeroAttacker Attacker { get; private set; }
        public TrainCharacter TrainCharacter { get; private set; }

        public AllStats Stats => _character.Stats;

        public Vector2 Position => _character.Position;
        public Vector2 PositionCenter => _character.PositionCenter;
        public Vector2 PositionCenterOffset => _character.PositionCenterOffset;

        [SerializeField] private Attack _attack;

        private bool _active;

        private HeroTrainStatEffects _trainStatEffects;
        private HeroAnimator _animator;
        private HeroInput _input;
        private HeroMoving _moving;
        private HeroPrincessGathering _princessGathering;

        private Character _character;

        private InitialStats _initialStats;

        private HeroSettings _settings;

        [Inject]
        public void Construct(InitialStats initialStats, HeroSettings settings)
        {
            _initialStats = initialStats;
            _settings = settings;
        }

        public void Initialize()
        {
            FillComponents();
            InitializeComponents();

            TrainCharacter.SetAsHero();
            _character.SetCustomHitInvulnerabilityTime(_settings.HitInvulnerabilityTime);

            _character.Slain += OnSlain;
        }

        public void Dispose()
        {
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

            _princessGathering.Tick();
        }

        public void PlaceAt(Vector3 position)
        {
            transform.position = position;
        }

        public void ApplyPrincessStatEffects(Trait effects)
        {
            _trainStatEffects.ApplyPrincessStatEffects(effects);
        }

        public void RemovePrincessStatEffects(Trait effects)
        {
            _trainStatEffects.RemovePrincessStatEffects(effects);
        }

        private void OnSlain()
        {
            Slain?.Invoke();
        }

        private void FillComponents()
        {
            _character = GetComponent<Character>();

            Attacker = GetComponent<HeroAttacker>();
            TrainCharacter = GetComponent<TrainCharacter>();

            _trainStatEffects = GetComponent<HeroTrainStatEffects>();
            _animator = GetComponent<HeroAnimator>();
            _input = GetComponent<HeroInput>();
            _moving = GetComponent<HeroMoving>();
            _princessGathering = GetComponent<HeroPrincessGathering>();
        }

        private void InitializeComponents()
        {
            _character.Initialize(_initialStats);

            Attacker.Initialize();
            TrainCharacter.Initialize();

            _trainStatEffects.Initialize();
            _animator.Initialize();
            _input.Initialize();
            _moving.Initialize();
            _princessGathering.Initialize();
        }

        private void DisposeComponents()
        {
            _character.Dispose();

            _animator.Dispose();
            _princessGathering.Dispose();
        }
    }
}
