using UnityEngine;

namespace Combat.Weapons
{
    public class WeaponAnimator
    {
        private static readonly int Horizontal = Animator.StringToHash("horizontal");
        private static readonly int Vertical = Animator.StringToHash("vertical");

        private readonly Animator _animator;

        public WeaponAnimator(Animator animator)
        {
            _animator = animator;
        }

        public void AlignWithCharacter(Vector2 velocity)
        {
            _animator.SetFloat(Horizontal, velocity.x);
            _animator.SetFloat(Vertical, velocity.y);
        }

        public void SetBool(int hashName, bool value)
        {
            _animator.SetBool(hashName, value);
        }
    }
}
