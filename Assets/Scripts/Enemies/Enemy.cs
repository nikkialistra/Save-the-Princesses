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

        public Vector2 Position => transform.position;

        public float ViewDistance => _specs.ViewDistance;

        public Hero Hero { get; private set; }
        public EnemyAttacker Attacker { get; set; }

        public CharacterMoving Moving => _character.Moving;

        private EnemySpecs _specs;

        private Character _character;

        private CharacterStats _characterStats;
        private MeleeStats _meleeStats;
        private RangedStats _rangedStats;

        [Inject]
        public void Construct(Hero hero)
        {
            Hero = hero;
        }

        public void Initialize(InitialStats initialStats, EnemySpecs specs)
        {
            _character = GetComponent<Character>();

            _specs = specs;

            InitializeComponents(initialStats);

            _character.Slain += OnSlain;
        }

        public void Dispose()
        {
            DisposeComponents();

            _character.Slain -= OnSlain;
        }

        public void Tick()
        {
            if (!Active) return;

            _character.Tick();
            Attacker.Tick();
        }

        public void FixedTick()
        {
            if (Active) return;

            _character.FixedTick();
        }

        public void SetWeapon(Weapon weapon)
        {
            Attacker.SetWeapon(weapon);
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

            Active = false;
        }

        private void InitializeComponents(InitialStats initialStats)
        {
            _character.Initialize(CharacterType.Enemy, initialStats);

            Attacker = new EnemyAttacker(_character, Hero);
        }

        private void DisposeComponents()
        {
            _character.Dispose();

            Attacker.Dispose();
        }
    }
}
