using UnityEngine;

namespace GameData.Princesses.Appearance.Elements
{
    [CreateAssetMenu(fileName = "(Princess Name)", menuName = "GameData/Princess Element Controllers")]
    public class PrincessElementControllers : ScriptableObject
    {
        public ElementControllers Heads = new();
        public ElementControllers Garments = new();
        public ElementControllers Hairs = new();
        public ElementControllers BodyAccessories = new();
        public ElementControllers HeadAccessories = new();
    }
}
