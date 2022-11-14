using Characters.Common;
using Cysharp.Threading.Tasks;
using Infrastructure.Installers.Game.Settings;
using Trains.Characters;
using UnityEngine;
using Zenject;

namespace Trains.HandConnections
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Hands : MonoBehaviour
    {
        [SerializeField] private TrainCharacter _trainCharacter;

        private TrainCharacter _nextTrainCharacter;

        private bool _linked;
        private HandsType _handsType;

        private SpriteRenderer _spriteRenderer;

        private HandsSprites _handsSprites;

        [Inject]
        public void Construct(HandsSprites handsSprites)
        {
            _handsSprites = handsSprites;
        }

        public void Initialize()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();

            _handsType = _trainCharacter.IsHero ? HandsType.HeroToPrincess : HandsType.PrincessToPrincess;

            _trainCharacter.NextChange += OnNextChange;
        }

        private void Update()
        {
            if (!_linked) return;

            UpdateLocation(_trainCharacter.PositionCenter, _nextTrainCharacter.PositionCenter);
        }

        public void Dispose()
        {
            _trainCharacter.NextChange -= OnNextChange;
        }

        private void OnNextChange()
        {
            if (_trainCharacter.Next != null)
                Link(_trainCharacter.Next);
            else
                Unlink();
        }

        private void Link(TrainCharacter nextTrainCharacter)
        {
            _nextTrainCharacter = nextTrainCharacter;

            _linked = true;

            WaitForLinkFinish().AttachExternalCancellation(this.GetCancellationTokenOnDestroy()).Forget();
        }

        private async UniTask WaitForLinkFinish()
        {
            while (DistanceNotCutDownEnough())
                await UniTask.Yield();

            if (_linked)
                _spriteRenderer.enabled = true;
        }

        private void Unlink()
        {
            _nextTrainCharacter = null;

            _linked = false;
            _spriteRenderer.enabled = false;
        }

        private bool DistanceNotCutDownEnough()
        {
            return _linked && Vector2.Distance(transform.position, _nextTrainCharacter.Position) >
                GameSettings.Princess.DistanceToFinishLinking;
        }

        private void UpdateLocation(Vector2 firstPosition, Vector2 secondPosition)
        {
            UpdatePosition(firstPosition, secondPosition);
            UpdateRotation(firstPosition, secondPosition);
        }

        private void UpdatePosition(Vector2 firstPosition, Vector2 secondPosition)
        {
            var centerPosition = (firstPosition + secondPosition) / 2f;

            transform.position = centerPosition;
        }

        private void UpdateRotation(Vector2 firstPosition, Vector2 secondPosition)
        {
            var direction = firstPosition - secondPosition;

            var snappedDirection = Direction9Utils.AnyDirectionToSnappedVector2(direction);
            var direction9 = Direction9Utils.Vector2ToDirection9(snappedDirection);

            UpdateSprite(direction9);
        }

        private void UpdateSprite(Direction9 direction9)
        {
            _spriteRenderer.sprite = _handsType == HandsType.HeroToPrincess
                ? _handsSprites.GetForHeroToPrincess(direction9)
                : _handsSprites.GetForPrincessToPrincess(direction9);
        }
    }
}
