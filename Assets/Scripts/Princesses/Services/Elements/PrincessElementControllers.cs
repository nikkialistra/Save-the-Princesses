using System;

namespace Princesses.Services.Elements
{
    [Serializable]
    public class PrincessElementControllers
    {
        public ElementControllers Heads = new();
        public ElementControllers Garments = new();
        public ElementControllers Hairs = new();
        public ElementControllers BodyAccessories = new();
        public ElementControllers HeadAccessories = new();
    }
}
