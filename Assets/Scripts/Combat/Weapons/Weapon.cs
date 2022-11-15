using Characters;
using Combat.Attacks;
using Combat.Weapons.Concrete;
using UnityEngine;

namespace Combat.Weapons
{
    [RequireComponent(typeof(ConcreteWeapon))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    public class Weapon : MonoBehaviour
    {
        private static readonly int BlinkId = Shader.PropertyToID("_Blink");

        public Attack Attack => _attack;
        public AttackLocation AttackLocation { get; private set; }
        public WeaponAnimator Animator { get; private set; }

        [SerializeField] private Attack _attack;

        private ConcreteWeapon _concreteWeapon;

        private SpriteRenderer _spriteRenderer;

        private Character _character;
        private CharacterBlinking _characterBlinking;

        public void Initialize(Character character, CharacterBlinking characterBlinking)
        {
            _character = character;
            _characterBlinking = characterBlinking;

            FillComponents();

            SubscribeToEvents();

            _attack.UpdateForWeaponSpecs(_concreteWeapon.Specs);
            _attack.Initialize(_character.PositionCenterOffset, _character.Type);
        }

        public void Dispose()
        {
            _attack.Dispose();

            UnsubscribeFromEvents();
        }

        public void Tick()
        {
            _attack.Tick();
        }

        private void AlignWithCharacter(CharacterAnimator.AnimationStatus status)
        {
            Animator.AlignWithCharacter(status.Velocity);
        }

        private void Show()
        {
            _spriteRenderer.enabled = true;
        }

        private void Hide()
        {
            _spriteRenderer.enabled = false;
        }

        private void Blink()
        {
            SetBlink(true);
        }

        private void EndBlink()
        {
            SetBlink(false);
        }

        private void SetBlink(bool active)
        {
            _spriteRenderer.materials[0].SetInt(BlinkId, active == true ? 1 : 0);
        }

        private void FillComponents()
        {
            Animator = new WeaponAnimator(GetComponent<Animator>());
            _concreteWeapon = GetComponent<ConcreteWeapon>();

            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void SubscribeToEvents()
        {
            _attack.Start += Hide;
            _attack.End += Show;

            _character.Animator.UpdateFinish += AlignWithCharacter;

            _characterBlinking.Blinking += Blink;
            _characterBlinking.BlinkingEnd += EndBlink;
        }

        private void UnsubscribeFromEvents()
        {
            _attack.Start -= Hide;
            _attack.End -= Show;

            _character.Animator.UpdateFinish -= AlignWithCharacter;

            _characterBlinking.Blinking -= Blink;
            _characterBlinking.BlinkingEnd -= EndBlink;
        }
    }
}
