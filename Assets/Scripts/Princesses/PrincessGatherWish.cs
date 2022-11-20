using System.Collections;
using Characters;
using UnityEngine;

namespace Princesses
{
    public class PrincessGatherWish
    {
        public bool Showing { get; private set; }

        private Coroutine _showGatherWithCoroutine;

        private readonly CharacterAnimator _animator;

        private readonly Princess _princess;

        public PrincessGatherWish(Princess princess, CharacterAnimator animator)
        {
            _princess = princess;
            _animator = animator;
        }

        public void Show()
        {
            if (Showing) return;

            Showing = true;
            _showGatherWithCoroutine = _princess.StartCoroutine(CShowGatherWish());
        }

        public void Hide()
        {
            if (!Showing) return;

            Showing = false;

            if (_showGatherWithCoroutine != null)
            {
                _princess.StopCoroutine(_showGatherWithCoroutine);
                _showGatherWithCoroutine = null;
            }

            _princess.HideHands();
        }

        private IEnumerator CShowGatherWish()
        {
            while (true)
            {
                var directionToHero = (_princess.ClosestHero.Position - _princess.Position).normalized;

                _animator.LookTo(directionToHero);

                yield return null;
            }
        }
    }
}
