using GameData.Settings.Types;
using UnityEngine;
using Zenject;

namespace GameData.Settings
{
    public class GameSettings : MonoBehaviour, IInitializable
    {
        public static GeneralSettings General { get; set; }
        public static CharacterSettings Character { get; set; }
        public static HeroSettings Hero { get; set; }
        public static PrincessSettings Princess { get; set; }

        [SerializeField] private GeneralSettings _generalSettings;
        [SerializeField] private CharacterSettings _characterSettings;
        [SerializeField] private HeroSettings _heroSettings;
        [SerializeField] private PrincessSettings _princessSettings;

        public void Initialize()
        {
            General = _generalSettings;
            Character = _characterSettings;
            Hero = _heroSettings;
            Princess = _princessSettings;
        }
    }
}
