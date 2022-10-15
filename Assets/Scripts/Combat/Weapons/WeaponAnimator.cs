using UnityEngine;

namespace Combat.Weapons
{
    [RequireComponent(typeof(Animator))]
    public class WeaponAnimator : MonoBehaviour
    {
        private static readonly int Horizontal = Animator.StringToHash("horizontal");
        private static readonly int Vertical = Animator.StringToHash("vertical");

        private Animator _animator;

        public void Initialize()
        {
            _animator = GetComponent<Animator>();
        }

        public void AlignWithCharacter(Vector2 velocity)
        {
            _animator.SetFloat(Horizontal, velocity.x);
            _animator.SetFloat(Vertical, velocity.y);
        }
    }
}
