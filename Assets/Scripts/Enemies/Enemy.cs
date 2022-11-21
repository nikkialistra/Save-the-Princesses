using System;
using Characters;
using Characters.Common;
using Characters.Moving;
using Characters.Stats.Character;
using Characters.Stats.Melee;
using Characters.Stats.Ranged;
using Combat.Weapons;
using Entities;
using GameData.Enemies;
using GameData.Stats;
using Heroes;
using Heroes.Services;
using Surrounding.Rooms;
using UnityEngine;
using Zenject;

namespace Enemies
{
    [RequireComponent(typeof(Character))]
    public class Enemy : MonoBehaviour, IEntity, ITickable, IFixedTickable
    {
        public event Action Slain;

        public bool Active { get; set; }

        public Character Character { get; private set; }

        public EnemyAttacker Attacker { get; set; }

        public Hero ClosestHero => _heroClosestFinder.GetFor(transform.position);

        public Vector2 Position => transform.position;

        public float ViewDistance => _specs.ViewDistance;

        public CharacterMoving Moving => Character.Moving;

        private EnemySpecs _specs;

        private CharacterStats _characterStats;
        private MeleeStats _meleeStats;
        private RangedStats _rangedStats;

        private HeroClosestFinder _heroClosestFinder;

        public void Initialize(HeroClosestFinder heroClosestFinder, InitialStats initialStats, EnemySpecs specs)
        {
            _heroClosestFinder = heroClosestFinder;

            Character = GetComponent<Character>();

            _specs = specs;

            InitializeComponents(initialStats);

            Character.Slain += OnSlain;
        }

        public void Dispose()
        {
            DisposeComponents();

            Character.Slain -= OnSlain;
        }

        public void Tick()
        {
            if (!Active) return;

            Character.Tick();
            Attacker.Tick();
        }

        public void FixedTick()
        {
            if (Active) return;

            Character.FixedTick();
        }

        public void SetWeapon(Weapon weapon)
        {
            Attacker.SetWeapon(weapon);
        }

        public void PlaceInRoom(Room room)
        {
            Character.PlaceInRoom(room);
        }

        public void SetPosition(Vector3 position, Transform parent)
        {
            transform.parent = parent;
            transform.position = position;
        }

        private void OnSlain()
        {
            Active = false;
            Slain?.Invoke();
        }

        private void InitializeComponents(InitialStats initialStats)
        {
            Character.Initialize(CharacterType.Enemy, initialStats);

            Attacker = new EnemyAttacker(Character, ClosestHero);
        }

        private void DisposeComponents()
        {
            Character.Dispose();

            Attacker.Dispose();
        }
    }
}
