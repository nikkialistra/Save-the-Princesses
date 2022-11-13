using Characters;
using UnityEngine;

namespace Heroes
{
    public class HeroMoving
    {
        private readonly HeroInput _input;
        private readonly CharacterMoving _moving;

        public HeroMoving(HeroInput input, CharacterMoving moving)
        {
            _input = input;
            _moving = moving;
        }

        public void Tick()
        {
            if (_input.MoveInput != Vector2.zero)
                _moving.Move(_input.DirectionNormalized);
            else
                _moving.Stop();
        }
    }
}
