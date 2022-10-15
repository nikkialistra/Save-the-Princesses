using System;
using Combat.Attacks;
using Combat.Weapons;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Heroes.Attacks
{
    public class HeroAttacker : MonoBehaviour
    {
        public event Action StrokeStart;

        [SerializeField] private ConcreteWeapon _concreteWeapon;

        [Space]
        [SerializeField] private Attack _attack;

        private PlayerInput _playerInput;

        private InputAction _attackAction;

        [Inject]
        public void Construct(PlayerInput playerInput)
        {
            _playerInput = playerInput;
        }

        public void Initialize()
        {
            _attackAction = _playerInput.actions.FindAction("Attack");
        }

        private void Update()
        {
            if (_attackAction.IsPressed())
                TryStroke();
        }

        private void TryStroke()
        {
            if (_concreteWeapon.TryStroke())
            {
                _attack.Do(_concreteWeapon.LastStroke);
                StrokeStart?.Invoke();
            }
        }
    }
}
