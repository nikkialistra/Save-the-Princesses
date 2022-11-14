using GameData.Settings.Types;

namespace GameData.Settings
{
    public class GameSettings
    {
        public static CharacterSettings Character;
        public static HeroSettings Hero;
        public static PrincessSettings Princess;

        public GameSettings(CharacterSettings character, HeroSettings hero, PrincessSettings princess)
        {
            Character = character;
            Hero = hero;
            Princess = princess;
        }
    }
}
