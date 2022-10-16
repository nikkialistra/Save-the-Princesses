using System;
using Cysharp.Threading.Tasks;
using Infrastructure.Installers.Game.Settings;
using UnityEngine;
using Zenject;

namespace Characters
{
    [RequireComponent(typeof(CharacterHealthHandling))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class CharacterBlinking : MonoBehaviour
    {
        private static readonly int BlinkId = Shader.PropertyToID("_Blink");

        public event Action Blinking;
        public event Action BlinkingEnd;

        private bool _active;

        private CharacterHealthHandling _healthHandling;
        private SpriteRenderer _spriteRenderer;

        private CharacterSettings _settings;

        [Inject]
        public void Construct(CharacterSettings settings)
        {
            _settings = settings;
        }

        public void Initialize()
        {
            FillComponents();

            _healthHandling.Hit += OnHit;
        }

        public void Dispose()
        {
            _healthHandling.Hit += OnHit;
        }

        private void OnHit()
        {
            if (!_active)
                Blink().AttachExternalCancellation(this.GetCancellationTokenOnDestroy()).Forget();
        }

        private async UniTask Blink()
        {
            _active = true;

            _spriteRenderer.materials[0].SetInt(BlinkId, 1);
            Blinking?.Invoke();

            await UniTask.Delay(TimeSpan.FromSeconds(_settings.HitBlinkingTime));

            _spriteRenderer.materials[0].SetInt(BlinkId, 0);
            BlinkingEnd?.Invoke();

            _active = false;
        }

        private void FillComponents()
        {
            _healthHandling = GetComponent<CharacterHealthHandling>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}
