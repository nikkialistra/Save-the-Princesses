using System;
using Characters;
using Entities;
using Heroes;
using Medium.Rooms;
using UnityEngine;
using Zenject;

namespace Enemies
{
    [RequireComponent(typeof(Character))]
    [RequireComponent(typeof(CharacterMoving))]
    [RequireComponent(typeof(EnemyAttacker))]
    public class Enemy : MonoBehaviour, IEntity
    {
        public event Action Spawn;
        public event Action Dying;

        public Vector2 Position => transform.position;

        public float ViewDistance => _viewDistance;

        public Hero Hero { get; private set; }
        public EnemyAttacker Attacker { get; set; }

        public CharacterMoving Moving { get; private set; }

        [SerializeField] private float _viewDistance = 7;

        private Character _character;

        [Inject]
        public void Construct(Hero hero)
        {
            Hero = hero;
        }

        public void Initialize()
        {
            FillComponents();
            InitializeComponents();

            _character.Dying += OnDying;

            Spawn?.Invoke();
        }

        public void Dispose()
        {
            DisposeComponents();

            _character.Dying -= OnDying;
        }

        public void PlaceInRoom(Room room)
        {
            _character.PlaceInRoom(room);
        }

        public void SetParent(Transform parent)
        {
            transform.parent = parent;
        }

        private void OnDying()
        {
            Dying?.Invoke();
        }

        private void FillComponents()
        {
            _character = GetComponent<Character>();

            Moving = GetComponent<CharacterMoving>();
            Attacker = GetComponent<EnemyAttacker>();
        }

        private void InitializeComponents()
        {
            _character.Initialize();

            Moving.Initialize();
            Attacker.Initialize();
        }

        private void DisposeComponents()
        {
            _character.Dispose();

            Moving.Dispose();
            Attacker.Dispose();
        }
    }
}
