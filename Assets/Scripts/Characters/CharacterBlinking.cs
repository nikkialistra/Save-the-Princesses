﻿using System;
using System.Collections;
using GameData.Settings;
using UnityEngine;

namespace Characters
{
    public class CharacterBlinking
    {
        private static readonly int BlinkId = Shader.PropertyToID("_Blink");

        public event Action<bool> BlinkChange;

        private bool _active;

        private readonly SpriteRenderer _spriteRenderer;

        private readonly Character _character;

        public CharacterBlinking(Character character, SpriteRenderer spriteRenderer)
        {
            _character = character;
            _spriteRenderer = spriteRenderer;

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
            BlinkChange?.Invoke(true);

            yield return new WaitForSeconds(GameSettings.Character.HitBlinkingTime);

            _spriteRenderer.materials[0].SetInt(BlinkId, 0);
            BlinkChange?.Invoke(false);

            _active = false;
        }
    }
}
