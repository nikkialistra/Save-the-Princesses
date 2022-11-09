using System;
using Characters;
using Characters.Stats.Character;
using Characters.Stats.Melee;
using Characters.Stats.Ranged;
using Entities;
using Heroes;
using Surrounding.Rooms;
using UnityEngine;
using Zenject;

namespace Enemies
{
    [RequireComponent(typeof(Character))]
    [RequireComponent(typeof(CharacterMoving))]
    [RequireComponent(typeof(CharacterStats))]
    [RequireComponent(typeof(MeleeStats))]
    [RequireComponent(typeof(RangedStats))]
    [RequireComponent(typeof(EnemyAttacker))]
    public class Enemy : MonoBehaviour, IEntity
    {
        public event Action Spawned;
        public event Action Slain;

        public Vector2 Position => transform.position;

        public float ViewDistance => _viewDistance;

        public Hero Hero { get; private set; }
        public EnemyAttacker Attacker { get; set; }

        public CharacterMoving Moving { get; private set; }

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

        public void Initialize()
        {
            FillComponents();
            InitializeComponents();

            _character.Slain += OnSlain;

            Spawned?.Invoke();
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

            _characterStats = GetComponent<CharacterStats>();
            _meleeStats = GetComponent<MeleeStats>();
            _rangedStats = GetComponent<RangedStats>();

            Moving = GetComponent<CharacterMoving>();
            Attacker = GetComponent<EnemyAttacker>();
        }

        private void InitializeComponents()
        {
            _character.Initialize();

            _characterStats.Initialize();
            _meleeStats.Initialize();
            _rangedStats.Initialize();

            Moving.Initialize();
            Attacker.Initialize();
        }

        private void DisposeComponents()
        {
            _character.Dispose();

            Moving.Dispose();
            Attacker.Dispose();
        }

        public class Factory : PlaceholderFactory<Enemy> { }
    }
}
