using System.Collections;
using Heroes;
using Princesses;
using Saving.Settings;
using UnityEngine;
using Zenject;
using GameSettings = GameData.Settings.GameSettings;

namespace Trains.HandConnections
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Hand : MonoBehaviour
    {
        private static float AngleToHero(Vector2 direction) => Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        private SpriteRenderer _spriteRenderer;

        private Princess _princess;
        private Hero _hero;

        private Coroutine _showingCoroutine;

        public void Initialize(Princess princess, Hero hero)
        {
            _princess = princess;
            _hero = hero;

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

                transform.position = _princess.PositionCenter + (direction * GameSettings.Princess.HandDistance);
                transform.rotation = Quaternion.Euler(0, 0, AngleToHero(direction));

                yield return null;
            }
        }
    }
}
