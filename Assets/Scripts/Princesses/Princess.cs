using System;
using Characters;
using Characters.Common;
using Characters.Moving;
using GameData.Stats;
using Heroes;
using Heroes.Services;
using Princesses.Types;
using Rooms;
using Rooms.Entities;
using Trains;
using Trains.Characters;
using Trains.HandConnections;
using UnityEngine;
using Zenject;

namespace Princesses
{
    [RequireComponent(typeof(Character))]
    [RequireComponent(typeof(TrainCharacter))]
    [RequireComponent(typeof(PrincessAnimators))]
    [RequireComponent(typeof(PrincessElementControllers))]
    [RequireComponent(typeof(PrincessMovingInTrain))]
    public class Princess : MonoBehaviour, IEntity, ITickable, IFixedTickable
    {
        public event Action Slain;

        public bool Active { get; set; }

        public Train Train { get; private set; }

        public TrainCharacter TrainCharacter { get; private set; }
        public CharacterMoving Moving => _character.Moving;
        public PrincessMovingInTrain MovingInTrain { get; private set; }

        public PrincessType Type => _type;

        public Hero ClosestHero => _heroClosestFinder.GetFor(transform.position);

        public bool Gathered { get; private set; }
        public bool ShowingGatherWish => _gatherWish.Showing;
        public bool Untied => !_tied.Tied;

        public bool Free => !_tied.Tied && !TrainCharacter.InTrainNow;

        public Vector2 PositionCenter => _character.PositionCenter;
        public Vector2 Position => transform.position;

        public float ActualVelocity => _actualVelocity.Value;

        [SerializeField] private PrincessType _type;
        [SerializeField] private Hand _hand;

        [SerializeField] private Trait _slowMoving;
        [SerializeField] private Trait _fastMoving;

        private Collider2D _collider;

        private float _regularSpeed;
        private bool _speedElevated;

        private PrincessAnimators _animators;
        private PrincessTied _tied;
        private PrincessGatherWish _gatherWish;
        private PrincessElementControllers _elements;
        private PrincessActualVelocity _actualVelocity;

        private Character _character;

        private HeroClosestFinder _heroClosestFinder;

        public void Initialize(HeroClosestFinder heroClosestFinder, InitialStats initialStats)
        {
            _heroClosestFinder = heroClosestFinder;

            _collider = GetComponent<CircleCollider2D>();

            FillComponents();
            InitializeComponents(initialStats);

            _regularSpeed = _character.Stats.MovementSpeedStat.BaseValue;

            SubscribeToEvents();
        }

        public void Dispose()
        {
            DisposeComponents();

            UnsubscribeFromEvents();
        }

        public void Tick()
        {
            if (!Active) return;

            _character.Tick();

            if (Gathered)
                TrainCharacter.Tick();
        }

        public void FixedTick()
        {
            if (!Active) return;

            _character.FixedTick();

            if (TrainCharacter.InTrainNow)
            {
                Moving.FixedTick();
                MovingInTrain.FixedTick();
            }

            _actualVelocity.FixedTick();
        }

        public void PlaceInRoom(Room room)
        {
            _character.PlaceInRoom(room);
        }

        public void SetPosition(Vector3 position, Transform parent)
        {
            transform.parent = parent;
            transform.position = position;
        }

        public void ShowGatherWish()
        {
            _gatherWish.Show();
        }

        public void HideGatherWish()
        {
            _gatherWish.Hide();
        }

        public void ShowHands()
        {
            _hand.Show();
        }

        public void HideHands()
        {
            _hand.Hide();
        }

        public void GatherTo(Train train)
        {
            Train = train;

            _character.RemoveTrait(_slowMoving);

            _gatherWish.Hide();
            Gathered = true;
        }

        public void TurnOffCollider()
        {
            _collider.enabled = false;
        }

        public void ChangeToRegularSpeed()
        {
            _character.Stats.MovementSpeedStat.ChangeBaseValue(_regularSpeed);
        }

        public void ChangeToHeroSpeed()
        {
            _character.Stats.MovementSpeedStat.ChangeBaseValue(ClosestHero.Stats.MovementSpeed);
        }

        public void ElevateSpeed()
        {
            if (_speedElevated) return;

            _character.AddTrait(_fastMoving);
            _speedElevated = true;
        }

        public void DeElevateSpeed()
        {
            if (!_speedElevated) return;

            _character.RemoveTrait(_fastMoving);
            _speedElevated = false;
        }

        private void ResetGathering()
        {
            _character.AddTrait(_slowMoving);

            Gathered = false;
        }

        private void TiedHit()
        {
            _animators.TiedHit();
        }

        private void Untie()
        {
            _animators.Untie();

            _character.AddTrait(_slowMoving);
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

            MovingInTrain = GetComponent<PrincessMovingInTrain>();

            _animators = GetComponent<PrincessAnimators>();
            _elements = GetComponent<PrincessElementControllers>();
        }

        private void InitializeComponents(InitialStats initialStats)
        {
            _character.Initialize(CharacterType.Princess, initialStats);

            TrainCharacter.Initialize(_character, _character.Moving);
            MovingInTrain.Initialize(Moving, _character.Stats, ClosestHero);

            _animators.Initialize();
            _gatherWish = new PrincessGatherWish(this, _character.Animator);
            _tied = new PrincessTied(this);
            _elements.Initialize(this, _character.Animator);
            _actualVelocity = new PrincessActualVelocity(this);

            _hand.Initialize(this, ClosestHero);
        }

        private void DisposeComponents()
        {
            _character.Dispose();

            TrainCharacter.Dispose();

            _elements.Dispose();
        }

        private void SubscribeToEvents()
        {
            _character.Slain += OnSlain;

            _tied.Hit += TiedHit;
            _tied.Untie += Untie;
        }

        private void UnsubscribeFromEvents()
        {
            _character.Slain -= OnSlain;

            _tied.Hit -= TiedHit;
            _tied.Untie -= Untie;

            TrainCharacter.TrainLeave += ResetGathering;
        }
    }
}
