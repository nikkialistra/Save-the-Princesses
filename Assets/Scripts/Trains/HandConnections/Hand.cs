using System.Collections;
using Characters.Common;
using Heroes;
using Princesses;
using UnityEngine;
using Zenject;

namespace Trains.HandConnections
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Hand : MonoBehaviour
    {
        [SerializeField] private Princess _princess;

        [SerializeField] private float _distance = 0.5f;

        private static float AngleToHero(Vector2 direction) => Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        private SpriteRenderer _spriteRenderer;

        private Hero _hero;

        private Coroutine _showingCoroutine;

        [Inject]
        public void Construct(Hero hero)
        {
            _hero = hero;
        }

        public void Initialize()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Show()
        {
            _showingCoroutine = StartCoroutine(CShowing());
            _spriteRenderer.enabled = true;
        }

        public void Hide()
        {
            _spriteRenderer.enabled = false;

            if (_showingCoroutine != null)
            {
                StopCoroutine(_showingCoroutine);
                _showingCoroutine = null;
            }
        }

        private IEnumerator CShowing()
        {
            while (true)
            {
                var direction = (_hero.PositionCenter - _princess.PositionCenter).normalized;

                transform.position = _princess.PositionCenter + (direction * _distance);
                transform.rotation = Quaternion.Euler(0, 0, AngleToHero(direction));

                yield return null;
            }
        }
    }
}
