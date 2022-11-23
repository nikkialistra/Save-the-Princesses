using Characters;
using Combat.Attacks;
using Combat.Weapons.Concrete;
using GameData.Weapons;
using UnityEngine;
using static Characters.CharacterAnimator;

namespace Combat.Weapons
{
    [RequireComponent(typeof(ConcreteWeapon))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    public class Weapon : MonoBehaviour
    {
        private static readonly int BlinkId = Shader.PropertyToID("_Blink");

        public Attack Attack => _attack;
        public float AttackRotation => _attack.Location.Rotation;
        public WeaponAnimator Animator { get; private set; }

        public StrokeType LastStroke => _concreteWeapon.LastStroke;

        [SerializeField] private Attack _attack;

        private ConcreteWeapon _concreteWeapon;

        private SpriteRenderer _spriteRenderer;

        private Character _character;

        public void Initialize(WeaponSpecs specs, Character parent)
        {
            transform.parent = parent.transform;

            _character = parent;

            FillComponents();

            _concreteWeapon.Initialize(specs);

            SubscribeToEvents();

            _attack.UpdateForWeaponSpecs(specs);
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

        public bool TryStroke()
        {
            return _concreteWeapon.TryStroke();
        }

        public void ResetStroke()
        {
            _concreteWeapon.ResetStroke();
        }

        private void AlignWithCharacter(AnimationStatus status)
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

        private void OnCharacterBlinkChange(bool active)
        {
            _spriteRenderer.materials[0].SetInt(BlinkId, active ? 1 : 0);
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

            _character.BlinkChange += OnCharacterBlinkChange;
        }

        private void UnsubscribeFromEvents()
        {
            _attack.Start -= Hide;
            _attack.End -= Show;

            _character.Animator.UpdateFinish -= AlignWithCharacter;

            _character.BlinkChange -= OnCharacterBlinkChange;
        }
    }
}
