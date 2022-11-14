﻿using System;
using Characters;
using Characters.Moving;
using Characters.Traits;
using Entities;
using GameData.Stats;
using Heroes;
using Princesses.Types;
using Surrounding.Rooms;
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
    [RequireComponent(typeof(PrincessTied))]
    [RequireComponent(typeof(PrincessGatherWish))]
    [RequireComponent(typeof(PrincessActualVelocity))]
    [RequireComponent(typeof(PrincessMovingInTrain))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Princess : MonoBehaviour, IEntity
    {
        public event Action Slain;

        public Train Train { get; private set; }

        public TrainCharacter TrainCharacter { get; private set; }
        public CharacterMoving Moving => _character.Moving;
        public PrincessMovingInTrain MovingInTrain { get; private set; }

        public PrincessType Type => _type;

        public bool Gathered { get; private set; }
        public bool ShowingGatherWish => _gatherWish.Showing;
        public bool Untied => !_tied.Tied;

        public bool Free => !_tied.Tied && !TrainCharacter.InTrainNow;

        public Vector2 PositionCenter => _character.PositionCenter;
        public Vector2 Position => transform.position;

        public float ActualVelocity => _actualVelocity.Value;

        [SerializeField] private PrincessType _type;
        [SerializeField] private Hand _hand;

        [Space]
        [SerializeField] private float _regularSpeed = 2f;

        [SerializeField] private Trait _slowMoving;
        [SerializeField] private Trait _fastMoving;

        [Space]
        [SerializeField] private Collider2D _collider;

        private Hero _hero;

        private bool _speedElevated;

        private PrincessAnimators _animators;
        private PrincessTied _tied;
        private PrincessGatherWish _gatherWish;
        private PrincessElementControllers _elements;
        private PrincessActualVelocity _actualVelocity;

        private Character _character;

        [Inject]
        public void Construct(Hero hero, Train train)
        {
            _hero = hero;
            Train = train;
        }

        public void Initialize(InitialStats initialStats)
        {
            FillComponents();
            InitializeComponents(initialStats);

            SubscribeToEvents();
        }

        public void Dispose()
        {
            DisposeComponents();

            UnsubscribeFromEvents();
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

        public void ShowGatherHands()
        {
            _gatherWish.ShowHands();
        }

        public void Gather()
        {
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
            _character.Stats.MovementSpeedStat.ChangeBaseValue(_hero.Stats.MovementSpeed);
        }

        public void ChangeToHeroSpeed()
        {
            _character.Stats.MovementSpeedStat.ChangeBaseValue(_regularSpeed);
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
            Slain?.Invoke();
        }

        private void FillComponents()
        {
            _character = GetComponent<Character>();

            TrainCharacter = GetComponent<TrainCharacter>();
            MovingInTrain = GetComponent<PrincessMovingInTrain>();

            _animators = GetComponent<PrincessAnimators>();
            _tied = GetComponent<PrincessTied>();
            _gatherWish = GetComponent<PrincessGatherWish>();
            _elements = GetComponent<PrincessElementControllers>();
            _actualVelocity = GetComponent<PrincessActualVelocity>();
        }

        private void InitializeComponents(InitialStats initialStats)
        {
            _character.Initialize(initialStats);

            TrainCharacter.Initialize();
            MovingInTrain.Initialize(Moving, _character.Stats);

            _animators.Initialize();
            _gatherWish.Initialize();
            _elements.Initialize();
            _actualVelocity.Initialize();

            _hand.Initialize();
        }

        private void DisposeComponents()
        {
            _character.Dispose();

            MovingInTrain.Dispose();

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
