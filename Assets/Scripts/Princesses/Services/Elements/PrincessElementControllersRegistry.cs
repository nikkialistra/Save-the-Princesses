using System;
using System.Collections.Generic;
using Princesses.Types;
using Sirenix.OdinInspector;
using UnityEngine;
using static Princesses.Types.ElementType;
using static Princesses.Types.PrincessType;

namespace Princesses.Services.Elements
{
    public class PrincessElementControllersRegistry : SerializedMonoBehaviour
    {
        [SerializeField] private PrincessElementControllers _defaultControllers;

        [Space]
        [SerializeField] private Dictionary<PrincessType, PrincessElementControllers> _customControllerMap = new();

        [Button(ButtonSizes.Medium), GUIColor(0.4f, 0.8f, 1)]
        private void AddNewElement()
        {
            _customControllerMap.Add(Standard, new PrincessElementControllers());
        }

        public RuntimeAnimatorController GetRandomFor(PrincessType princessType, ElementType elementType)
        {
            if (princessType == Standard)
                return GetDefaultRandomFor(elementType);

            if (_customControllerMap.ContainsKey(princessType) == false)
                return GetDefaultRandomFor(elementType);

            var customControllers = _customControllerMap[princessType];

            return GetFromCustomOrDefault(elementType, customControllers);
        }

        private RuntimeAnimatorController GetDefaultRandomFor(ElementType elementType)
        {
            var elementControllers = elementType switch {
                Head => _defaultControllers.Heads,
                Garment => _defaultControllers.Garments,
                Hair => _defaultControllers.Hairs,
                BodyAccessory => _defaultControllers.BodyAccessories,
                HeadAccessory => _defaultControllers.HeadAccessories,
                _ => throw new ArgumentOutOfRangeException()
            };

            return elementControllers.GetRandom();
        }

        private RuntimeAnimatorController GetFromCustomOrDefault(ElementType elementType, PrincessElementControllers customControllers)
        {
            var elementControllers = elementType switch
            {
                Head => customControllers.Heads,
                Garment => customControllers.Garments,
                Hair => customControllers.Hairs,
                BodyAccessory => customControllers.BodyAccessories,
                HeadAccessory => customControllers.HeadAccessories,
                _ => throw new ArgumentOutOfRangeException()
            };

            if (elementControllers.Empty)
                return GetDefaultRandomFor(elementType);

            return elementControllers.GetRandom();
        }
    }
}
