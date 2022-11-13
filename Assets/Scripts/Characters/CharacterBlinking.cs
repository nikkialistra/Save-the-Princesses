using System;
using System.Collections;
using Infrastructure.Installers.Game.Settings;
using UnityEngine;

namespace Characters
{
    public class CharacterBlinking
    {
        private static readonly int BlinkId = Shader.PropertyToID("_Blink");

        public event Action Blinking;
        public event Action BlinkingEnd;

        private bool _active;

        private readonly SpriteRenderer _spriteRenderer;

        private readonly Character _character;

        private readonly CharacterSettings _settings;

        public CharacterBlinking(Character character, SpriteRenderer spriteRenderer, CharacterSettings settings)
        {
            _character = character;
            _spriteRenderer = spriteRenderer;
            _settings = settings;

            _character.Hit += OnHit;
        }

        public void Dispose()
        {
            _character.Hit += OnHit;
        }

        private void OnHit()
        {
            if (!_active)
                _character.StartCoroutine(Blink());
        }

        private IEnumerator Blink()
        {
            _active = true;

            _spriteRenderer.materials[0].SetInt(BlinkId, 1);
            Blinking?.Invoke();

            yield return new WaitForSeconds(_settings.HitBlinkingTime);

            _spriteRenderer.materials[0].SetInt(BlinkId, 0);
            BlinkingEnd?.Invoke();

            _active = false;
        }
    }
}
