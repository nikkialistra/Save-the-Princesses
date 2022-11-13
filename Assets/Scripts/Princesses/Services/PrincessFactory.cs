using System.Collections.Generic;
using Princesses.Types;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Princesses.Services
{
    public class PrincessFactory : SerializedMonoBehaviour
    {
        [SerializeField] private Dictionary<PrincessType, Princess> _princessesMap = new();

        private DiContainer _diContainer;

        [Inject]
        public void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public Princess Create(PrincessType princessType)
        {
            var princessPrefab = _princessesMap[princessType];

            var pri = _diContainer.InstantiatePrefabForComponent<Princess>(princessPrefab);

            return pri;
        }
    }
}
