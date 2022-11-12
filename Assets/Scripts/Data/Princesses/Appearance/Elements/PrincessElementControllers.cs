using UnityEngine;

namespace Data.Princesses.Appearance.Elements
{
    [CreateAssetMenu(fileName = "(Princess Name)", menuName = "Data/Princess Element Controllers")]
    public class PrincessElementControllers : ScriptableObject
    {
        public ElementControllers Heads = new();
        public ElementControllers Garments = new();
        public ElementControllers Hairs = new();
        public ElementControllers BodyAccessories = new();
        public ElementControllers HeadAccessories = new();
    }
}
