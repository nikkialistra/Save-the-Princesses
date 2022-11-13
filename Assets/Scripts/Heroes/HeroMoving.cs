using Characters.Moving;
using UnityEngine;

namespace Heroes
{
    public class HeroMoving
    {
        private readonly HeroInput _input;
        private readonly CharacterMoving _characterMoving;

        public HeroMoving(HeroInput input, CharacterMoving characterMoving)
        {
            _input = input;
            _characterMoving = characterMoving;
        }

        public void Tick()
        {
            if (_input.MoveInput != Vector2.zero)
                _characterMoving.Move(_input.DirectionNormalized);
            else
                _characterMoving.Stop();
        }
    }
}
