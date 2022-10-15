using System.Collections;
using Characters;
using Heroes;
using Trains.HandConnections;
using UnityEngine;
using Zenject;

namespace Princesses
{
    [RequireComponent(typeof(CharacterAnimator))]
    public class PrincessGatherWish : MonoBehaviour
    {
        public bool Showing { get; private set; }

        [SerializeField] private Hand _hand;

        private Coroutine _showGatherWithCoroutine;

        private Hero _hero;

        private CharacterAnimator _animator;
        private SpriteRenderer _spriteRenderer;

        [Inject]
        private void Construct(Hero hero)
        {
            _hero = hero;
        }

        public void Initialize()
        {
            _animator = GetComponent<CharacterAnimator>();
        }

        public void Show()
        {
            if (Showing) return;

            Showing = true;
            _showGatherWithCoroutine = StartCoroutine(CShowGatherWish());
        }

        public void Hide()
        {
            if (!Showing) return;

            Showing = false;

            if (_showGatherWithCoroutine != null)
            {
                StopCoroutine(_showGatherWithCoroutine);
                _showGatherWithCoroutine = null;
            }

            HideHands();
        }

        public void ShowHands()
        {
            _hand.Show();
        }

        private void HideHands()
        {
            _hand.Hide();
        }

        private IEnumerator CShowGatherWish()
        {
            while (true)
            {
                var directionToHero = (_hero.Position - (Vector2)transform.position).normalized;

                _animator.LookTo(directionToHero);

                yield return null;
            }
        }
    }
}
