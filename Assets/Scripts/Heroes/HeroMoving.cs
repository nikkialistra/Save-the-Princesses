using Characters;
using UnityEngine;

namespace Heroes
{
    [RequireComponent(typeof(HeroInput))]
    [RequireComponent(typeof(CharacterMoving))]
    public class HeroMoving : MonoBehaviour
    {
        private HeroInput _input;
        private CharacterMoving _moving;

        public void Initialize()
        {
            _input = GetComponent<HeroInput>();
            _moving = GetComponent<CharacterMoving>();
        }

        private void FixedUpdate()
        {
            if (_input.MoveInput != Vector2.zero)
                _moving.Move(_input.DirectionNormalized);
            else
                _moving.Stop();
        }
    }
}
