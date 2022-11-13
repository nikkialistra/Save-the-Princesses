using System;
using Characters;
using Characters.Stats.Character;
using Characters.Stats.Melee;
using Characters.Stats.Ranged;
using Entities;
using GameData.Stats;
using Heroes;
using Surrounding.Rooms;
using UnityEngine;
using Zenject;

namespace Enemies
{
    [RequireComponent(typeof(Character))]
    [RequireComponent(typeof(EnemyAttacker))]
    public class Enemy : MonoBehaviour, IEntity
    {
        public event Action Slain;

        public Vector2 Position => transform.position;

        public float ViewDistance => _viewDistance;

        public Hero Hero { get; private set; }
        public EnemyAttacker Attacker { get; set; }

        public CharacterMoving Moving => _character.Moving;

        [SerializeField] private float _viewDistance = 7;

        private Character _character;

        private CharacterStats _characterStats;
        private MeleeStats _meleeStats;
        private RangedStats _rangedStats;

        [Inject]
        public void Construct(Hero hero)
        {
            Hero = hero;
        }

        public void Initialize(InitialStats initialStats)
        {
            FillComponents();
            InitializeComponents(initialStats);

            _character.Slain += OnSlain;
        }

        public void Dispose()
        {
            DisposeComponents();

            _character.Slain -= OnSlain;
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

        private void OnSlain()
        {
            Slain?.Invoke();
        }

        private void FillComponents()
        {
            _character = GetComponent<Character>();

            Attacker = GetComponent<EnemyAttacker>();
        }

        private void InitializeComponents(InitialStats initialStats)
        {
            _character.Initialize(initialStats);

            Attacker.Initialize();
        }

        private void DisposeComponents()
        {
            _character.Dispose();

            Attacker.Dispose();
        }
    }
}
