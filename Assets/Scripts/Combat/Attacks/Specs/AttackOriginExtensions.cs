using System;
using Characters;
using Characters.Common;

namespace Combat.Attacks.Specs
{
    public static class AttackOriginExtensions
    {
        public static bool IsFriendlyFor(this AttackOrigin attackOrigin, CharacterType characterType)
        {
            return attackOrigin switch
            {
                AttackOrigin.Hero => IsFriendlyForHero(characterType),
                AttackOrigin.Enemy => IsFriendlyForEnemy(characterType),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private static bool IsFriendlyForHero(CharacterType characterType)
        {
            return characterType switch
            {
                CharacterType.Hero => true,
                CharacterType.Princess => true,
                CharacterType.Enemy => false,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private static bool IsFriendlyForEnemy(CharacterType characterType)
        {
            return characterType switch
            {
                CharacterType.Hero => false,
                CharacterType.Princess => true,
                CharacterType.Enemy => true,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
