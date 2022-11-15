using Characters;
using Combat.Attacks;
using Combat.Weapons;
using Combat.Weapons.Concrete;
using Heroes;
using UnityEngine;
using Zenject;

namespace Enemies
{
    [RequireComponent(typeof(Character))]
    public class EnemyAttacker : MonoBehaviour
    {
        public float AttackDistance => _attack.AttackDistance;

        [SerializeField] private ConcreteWeapon _concreteWeapon;

        [Space]
        [SerializeField] private Attack _attack;

        private Vector2 DirectionToHero => _hero.PositionCenter - ((Vector2)transform.position + _attack.CenterOffset);
        private float AngleToHero => Mathf.Atan2(DirectionToHero.y, DirectionToHero.x) * Mathf.Rad2Deg;

        private Hero _hero;
        private Character _character;

        [Inject]
        public void Construct(Hero hero)
        {
            _hero = hero;
        }

        public void Initialize()
        {
            _character = GetComponent<Character>();

            _character.AtStun += OnAtStun;
        }

        public void Dispose()
        {
            _character.AtStun -= OnAtStun;
        }

        public void UpdateAttackRotation()
        {
            _attack.UpdateRotation(AngleToHero);
        }

        public void TryAttack()
        {
            if (_concreteWeapon.TryStroke())
                _attack.Do(_concreteWeapon.LastStroke);
        }

        private void CancelAttack()
        {
            _concreteWeapon.ResetStroke();
            _attack.Cancel();
        }

        private void OnAtStun()
        {
            CancelAttack();
        }
    }
}
